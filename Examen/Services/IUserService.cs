using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Examen.Database.Tables;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Examen.Services
{
    public class JwtToken
    {
        public long exp { get; set; }
    }

    public class AuthenticateModel
    {
        public bool status { set; get; }
        public User user { set; get; }
        public string token { set; get; }
    }

    public interface IUserService
    {
        AuthenticateModel AuthenticateWithAccount(string email, string password);

    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private IJsonSerializer _serializer = new JsonNetSerializer();
        private IDateTimeProvider _provider = new UtcDateTimeProvider();
        private IBase64UrlEncoder _urlEncoder = new JwtBase64UrlEncoder();
        private IJwtAlgorithm _algorithm = new HMACSHA256Algorithm();

        public DateTime GetExpiryTimestamp(string accessToken)
        {
            try
            {
                IJwtValidator _validator = new JwtValidator(_serializer, _provider);
                IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
                var token = decoder.DecodeToObject<JwtToken>(accessToken);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(token.exp);
                return dateTimeOffset.LocalDateTime;
            }
            catch (TokenExpiredException ex)
            {
                LOG.WriteLine($"[TokenExpiredException] {ex}");
                return DateTime.MinValue;
            }
            catch (SignatureVerificationException ex)
            {
                LOG.WriteLine($"[SignatureVerificationException] {ex}");
                return DateTime.MinValue;
            }
            catch (Exception ex)
            {
                LOG.WriteLine($"[GetExpiryTimestamp] {ex}");
                return DateTime.MinValue;
            }
        }

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateModel AuthenticateWithAccount(string email, string password)
        {
            AuthenticateModel model = new AuthenticateModel();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return model;
            }

            var client = User.Get(email.Trim(), password.Trim());

            // return null if user not found
            if (client == null)
            {
                return model;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, client.id.ToString()),
                    new Claim(ClaimTypes.Role, "user")
                }),
                Expires = DateTime.UtcNow.AddYears(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            model.status = true;
            model.user = client;
            //Generate token if empty
            var token = tokenHandler.CreateToken(tokenDescriptor);
            model.token = tokenHandler.WriteToken(token);

            return model;
        }
    }
}

