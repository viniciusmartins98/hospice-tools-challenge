using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospiceToolsChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public abstract class ApiControllerBase<T> : ControllerBase
    {
        public ILogger<T> Logger => HttpContext.RequestServices.GetRequiredService<ILogger<T>>();

        public IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();

        public IConfiguration Configuration => HttpContext.RequestServices.GetRequiredService<IConfiguration>();
    }
}
