using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Examen;
using Examen.Services;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// configure DI for application services
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSingleton<IUserIdProvider, NameUserIdProvider>();

// configure strongly typed settings objects
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["JwtKey"]));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(30);
});

// configure jwt authentication
var appSettings = appSettingsSection.Get<AppSettings>();


builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
.AddAuthentication(auth =>
 {
     auth.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
     auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
     auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
 }).AddJwtBearer(options =>
 {
     options.RequireHttpsMetadata = false;
     options.SaveToken = true;

     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = key,
         ValidateIssuer = false,
         ValidateAudience = false,

     };

 }).AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.UseAuthentication();
app.UseCookiePolicy();

//Add JWToken to all incoming HTTP Request Header
app.Use(async (context, next) =>
{
    if (string.IsNullOrWhiteSpace(context.Request.Headers["Authorization"]) && context.Request.QueryString.HasValue)
    {
        var token = context.Request.QueryString.Value.Split('&').SingleOrDefault(x => x.Contains("access_token"))?.Split('=')[1];
        if (!string.IsNullOrWhiteSpace(token))
        {
            context.Request.Headers.Add("Authorization", new[] { $"Bearer {token}" });
        }
    }
    await next.Invoke();

});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


#region Base_de_datos
DBManager.Init(app);
#endregion

app.Run();
