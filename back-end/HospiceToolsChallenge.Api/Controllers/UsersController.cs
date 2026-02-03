using HospiceToolsChallenge.Application.Queries.Users;
using Microsoft.AspNetCore.Mvc;

namespace HospiceToolsChallenge.Api.Controllers
{
    public class UsersController : ApiControllerBase<UsersController>
    {
        [HttpGet("auth")]
        public async Task<IActionResult> GetAuthenticatedUser()
        {
            var user = await Mediator.Send(new GetAuthenticatedUserQuery());
            return Ok(user);
        }
    }
}
