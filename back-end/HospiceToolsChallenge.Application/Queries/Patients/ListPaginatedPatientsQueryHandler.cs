using HospiceToolsChallenge.Application.Models.Patients;
using HospiceToolsChallenge.Application.Repositories;
using HospiceToolsChallenge.Domain.Pagination;
using MediatR;

namespace HospiceToolsChallenge.Application.Queries.Patients
{
    public class ListPaginatedPatientsQueryHandler(IPatientRepository repository) : IRequestHandler<ListPaginatedPatientsQuery, PaginatedResult<PatientModel>>
    {
        public async Task<PaginatedResult<PatientModel>> Handle(ListPaginatedPatientsQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.ListPaginatedAsync(request, cancellationToken);
            return new PaginatedResult<PatientModel>
            {
                CurrentPage = result.CurrentPage,
                Data = result.Data.Select(x => x.ToModel()),
                PageSize = result.PageSize,
                TotalItens = result.TotalItens,
                TotalPages = result.TotalPages
            };
        }
    }
}
