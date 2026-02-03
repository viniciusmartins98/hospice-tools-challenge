using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HospiceToolsChallenge.Application.UserContext;

namespace HospiceToolsChallenge.Api.Identity
{
    public class UserContext : IUserContext
    {
        public string Name { get; }
        public string Username { get; }

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            var claimsPrincipal = httpContextAccessor.HttpContext?.User;
            Username = claimsPrincipal?.FindFirstValue(JwtRegisteredClaimNames.Sub);
            Name = claimsPrincipal?.FindFirstValue(JwtRegisteredClaimNames.Name);
        }
    }
}
