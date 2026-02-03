using HospiceToolsChallenge.Application.Models.Patients;
using HospiceToolsChallenge.Application.Queries.Patients;
using HospiceToolsChallenge.Domain.Pagination;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HospiceToolsChallenge.Api.Controllers
{
    public class PatientsController : ApiControllerBase<PatientsController>
    {
        [Produces<Ok<PaginatedResult<PatientModel>>>]
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListPaginatedPatientsQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
