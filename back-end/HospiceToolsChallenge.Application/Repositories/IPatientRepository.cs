using HospiceToolsChallenge.Domain.Entities;
using HospiceToolsChallenge.Domain.Filters;
using HospiceToolsChallenge.Domain.Pagination;

namespace HospiceToolsChallenge.Application.Repositories
{
    public interface IPatientRepository
    {
        Task AddAsync(Patient patient, CancellationToken cancellationToken);

        Task<PaginatedResult<Patient>> ListPaginatedAsync(PaginatedFilter<PatientFilter> filter, CancellationToken cancellationToken);
    }
}
