
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HospiceToolsChallenge.Api.Filters
{
    public class UnhandledExceptionFilter(ILogger<UnhandledExceptionFilter> logger) : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is not ValidationException validationException)
            {
                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = context.Exception.Message ?? "Internal Server Error",
                    Detail = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
                };

                context.Result = new ObjectResult(problemDetails);
                context.ExceptionHandled = true;
                logger.LogError(context.Exception, context.Exception.Message);
                
            }
        }
    }
}
