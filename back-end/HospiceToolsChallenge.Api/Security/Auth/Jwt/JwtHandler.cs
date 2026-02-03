using Microsoft.IdentityModel.Tokens;
using HospiceToolsChallenge.Api.Security.Auth.Jwt.Models;
using HospiceToolsChallenge.Application.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospiceToolsChallenge.Api.Security.Auth.Jwt
{
    public class JwtHandler(
        IConfiguration configuration,
        IHttpContextAccessor httpAccessor)
    {
        public AuthTokenResponse GenerateAuthTokenResponse(UserModel user)
        {
            return new AuthTokenResponse
            {
                AccessToken = this.GenerateJwtToken(new JwtModel
                {
                    Claims = GetClaimsFromUser(user),
                    ExpirationMinutes = configuration.GetValue<int>("Jwt:ExpirationMinutes"),
                    Issuer = configuration.GetValue<string>("Jwt:Issuer"),
                    Secret = configuration.GetValue<string>("Jwt:Secret")
                })
            };
        }

        private string GenerateJwtToken(JwtModel jwtModel)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtModel.Secret));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtModel.Issuer,
                claims: jwtModel.Claims,
                expires: DateTime.UtcNow.AddMinutes(jwtModel.ExpirationMinutes),
                signingCredentials: signingCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }

        private List<Claim> GetClaimsFromUser(UserModel user)
        {
            return [
                new Claim(JwtRegisteredClaimNames.Sub, user.Username.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
            ];
        }

        public string RefreshJwtToken()
        {
            if (httpAccessor.HttpContext.User?.Identity?.IsAuthenticated == false)
            {
                return string.Empty;
            }
            List<string> userClaims = [JwtRegisteredClaimNames.Sub, JwtRegisteredClaimNames.Name];
            var claims = httpAccessor.HttpContext.User.Claims.Where(x => userClaims.Contains(x.Type));
            return GenerateJwtToken(new JwtModel
            {
                Claims = claims,
                ExpirationMinutes = configuration.GetValue<int>("Jwt:ExpirationMinutes"),
                Issuer = configuration.GetValue<string>("Jwt:Issuer"),
                Secret = configuration.GetValue<string>("Jwt:Secret")
            });
        }
    }
}
