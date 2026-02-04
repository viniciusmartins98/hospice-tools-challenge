using HospiceToolsChallenge.Application.Queries.Colors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospiceToolsChallenge.Api.Controllers
{
    public class ColorsController : ApiControllerBase<ColorsController>
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var colors = await Mediator.Send(new ListColorsQuery());
            return Ok(colors);
        }
    }
}
