using HospiceToolsChallenge.Application.Commands.Patients;
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

        [Produces<Created>]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddPatientCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Created();
        }

        [Produces<NoContent>]
        [HttpPut("{patientId}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid patientId,
            [FromBody] UpdatePatientDto request,
            CancellationToken cancellationToken)
        {
            await Mediator.Send(new UpdatePatientCommand
            {
                PatientId = patientId,
                FavoriteColorId = request.FavoriteColorId,
                FirstName = request.FirstName,
                Gender = request.Gender,
                Age = request.Age,
                LastName = request.LastName
            }, cancellationToken);
            return NoContent();
        }

        [Produces<NoContent>]
        [HttpDelete("{patientId}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid patientId,
            CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeletePatientCommand
            {
                PatientId = patientId
            }, cancellationToken);
            return NoContent();
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> Statistics()
        {
            var result = await Mediator.Send(new GetPatientStatisticsQuery());
            return Ok(result);
        }
    }
}
