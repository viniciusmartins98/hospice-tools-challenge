using HospiceToolsChallenge.Application.Models.Patients;
using HospiceToolsChallenge.Domain.Entities;
using HospiceToolsChallenge.Domain.Filters;
using HospiceToolsChallenge.Domain.Pagination;

namespace HospiceToolsChallenge.Application.Repositories
{
    public interface IPatientRepository
    {
        Task<PaginatedResult<Patient>> ListPaginatedAsync(PaginatedFilter<PatientFilter> filter, CancellationToken cancellationToken);
        Task AddAsync(Patient patient, CancellationToken cancellationToken);
        Task UpdateAsync(Guid id, UpdatePatientDto model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
