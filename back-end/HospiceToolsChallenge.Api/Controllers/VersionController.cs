using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospiceToolsChallenge.Api.Controllers
{
    public class VersionController : ApiControllerBase<VersionController>
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetVersion()
        {
            Logger.LogInformation("Getting app version");
            return Ok(new { Version = "1.0.0" });
        }
    }
}
