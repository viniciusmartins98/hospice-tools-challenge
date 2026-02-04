using HospiceToolsChallenge.Application.Repositories;
using HospiceToolsChallenge.Domain.Entities;
using HospiceToolsChallenge.Domain.Filters;
using HospiceToolsChallenge.Domain.Pagination;
using HospiceToolsChallenge.Infra.Extensions;
using HospiceToolsChallenge.Infra.Persistence;
using HospiceToolsChallenge.Infra.Persistence.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HospiceToolsChallenge.Infra.Repositories
{
    public class PatientRepository(DatabaseContext dbContext) : IPatientRepository
    {
        public async Task AddAsync(Patient patient, CancellationToken cancellationToken)
        {
            var entity = patient.MapToDb();
            entity.Id = Guid.NewGuid();
            await dbContext.PatientDb.AddAsync(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<PaginatedResult<Patient>> ListPaginatedAsync(PaginatedFilter<PatientFilter> filter, CancellationToken cancellationToken)
        {
            var patientNameFilter = filter.Filter.PatientName;
            return await dbContext.PatientDb
                .Where(x => patientNameFilter == null || x.FirstName.ToUpper().Contains(patientNameFilter.ToUpper()) || x.LastName.ToUpper().Contains(patientNameFilter.ToUpper()))
                .Include(x => x.FavoriteColor)
                .Select(x => x.MapToEntity())
                .ToPagedResultAsync(filter, cancellationToken);
        }
    }
}
