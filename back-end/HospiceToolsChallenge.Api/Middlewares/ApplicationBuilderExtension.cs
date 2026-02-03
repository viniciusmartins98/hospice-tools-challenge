namespace HospiceToolsChallenge.Api.Middlewares
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AccessTokenRefreshMiddleware>();
        }
    }
}
