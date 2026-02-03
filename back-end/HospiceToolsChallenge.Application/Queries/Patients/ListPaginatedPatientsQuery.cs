using HospiceToolsChallenge.Application.Models.Patients;
using HospiceToolsChallenge.Domain.Filters;
using HospiceToolsChallenge.Domain.Pagination;
using MediatR;

namespace HospiceToolsChallenge.Application.Queries.Patients
{
    public class ListPaginatedPatientsQuery : PaginatedFilter<PatientFilter>, IRequest<PaginatedResult<PatientModel>>
    {
    }
}
