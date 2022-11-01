using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Examen.Services;
using System.Diagnostics;
using System.Security.Claims;
using Examen.Database.Tables;

namespace Examen.Controllers
{
    public class AccountController : Controller
    {
        //[BindProperty]
        //public LoginViewModel Login { get; set; }
        IUserService _loginService;
        public AccountController(IUserService loginService)
        {
            _loginService = loginService;
        }

        // GET: AccountController
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            return Redirect("/alumnos");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(IFormCollection form)
        {
            try
            {
                // Verification.  
                if (ModelState.IsValid)
                {
                    string email = form["umail"];
                    string password = form["psw"];

                    // Initialization.  
                    var loginInfo = _loginService.AuthenticateWithAccount(email, password);
       
                    // Verification.  
                    if (!loginInfo.status)
                    {
                        LOG.WriteLine($"[loginInfo] {loginInfo.status}");
                        // Setting.  
                        ModelState.AddModelError("CustomError", "Invalid username or password.");
                        return View("Index", ModelState);
                    }
                    else
                    {

                        // Login In.  
                        bool status = await this.SignInUser(loginInfo.user, false);

                        if (!status)
                        {
                            ModelState.AddModelError("CustomError", "Invalid username or password.");
                            return View("Index", ModelState);
                        }

                        // Info.  
                        return this.Redirect("/account");

                    }
                }
            }
            catch (Exception ex)
            {
                LOG.WriteLine($"[LOGIN] {ex}");
            }

            return View("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties());

            return this.Redirect("/");
        }

       async Task<bool> SignInUser(User user, bool isPersistent)
        {
            try
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                var principal = new ClaimsPrincipal(identity);
                var authenticationManager = this.HttpContext;

                // Sign In.  
                await authenticationManager.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistent });
                return true;
            }
            catch (Exception ex)
            {
                // Info  
                LOG.WriteLine($"[SignInUser] {ex}");
                return false;
            }
        }
    }
}
