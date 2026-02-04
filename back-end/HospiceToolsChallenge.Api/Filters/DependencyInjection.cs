using Microsoft.AspNetCore.Mvc;

namespace HospiceToolsChallenge.Api.Filters
{
    public static class DependencyInjection
    {
        public static MvcOptions AddFilters(this MvcOptions options)
        {
            options.Filters.Add<ValidationExceptionFilter>();
            options.Filters.Add<UnhandledExceptionFilter>();
            return options;
        }
    }
}
