using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HospiceToolsChallenge.Api.Security.Auth.Jwt;
using HospiceToolsChallenge.Api.Security.Auth.Jwt.Models;
using HospiceToolsChallenge.Application.Queries.Auth;
using HospiceToolsChallenge.Api.Models;

namespace HospiceToolsChallenge.Api.Controllers
{
    public class AuthController : ApiControllerBase<AuthController>
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] AuthTokenRequest request, [FromServices] JwtHandler jwtHandler)
        {
            var user = await Mediator.Send(new GetUserByCredentialsQuery
            {
                Username = request.Username,
                Password = request.Password
            });

            if (user == null)
            {
                return Unauthorized(new DefaultResult
                {
                    Message = "Invalid credentials"
                });
            }
            return Ok(jwtHandler.GenerateAuthTokenResponse(user));
        }

        [HttpGet("refreshToken")]
        public IActionResult RefreshToken([FromBody] AuthTokenRequest request)
        {
            return NoContent();
        }
    }
}
