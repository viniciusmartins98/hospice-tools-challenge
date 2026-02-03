using HospiceToolsChallenge.Api.Security.Auth.Jwt;
using HospiceToolsChallenge.Application.Models.Users;
using HospiceToolsChallenge.Application.UserContext;

namespace HospiceToolsChallenge.Api.Middlewares
{
    public class AccessTokenRefreshMiddleware(
        RequestDelegate _next,
        ILogger<AccessTokenRefreshMiddleware> logger,
        JwtHandler jwtHandler
    )
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User?.Identity?.IsAuthenticated == false)
            {
                await _next(context);
                return;
            }
            var accessToken = jwtHandler.RefreshJwtToken();
            context.Response.Headers.Remove("x-access-token");
            context.Response.Headers.Append("x-access-token", accessToken);
            await _next(context);
        }
    }
}
