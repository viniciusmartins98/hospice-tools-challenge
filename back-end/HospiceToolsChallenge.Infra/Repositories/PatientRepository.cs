using HospiceToolsChallenge.Application.Models.Patients;
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
            await dbContext.PatientDb.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<PaginatedResult<Patient>> ListPaginatedAsync(PaginatedFilter<PatientFilter> filter, CancellationToken cancellationToken)
        {
            var patientNameFilter = filter.Filter.PatientName;
            return await dbContext.PatientDb
                .Where(x => patientNameFilter == null || 
                    (x.FirstName + " " + x.LastName).ToUpper().Contains(patientNameFilter.ToUpper()))
                .OrderBy(x => x.FirstName.ToUpper())
                    .ThenBy(x => x.LastName.ToUpper())
                .Include(x => x.FavoriteColor)
                .Select(x => x.MapToEntity())
                .ToPagedResultAsync(filter, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, UpdatePatientDto model, CancellationToken cancellationToken)
        {
            var gender = model.Gender?.ToString();
            await dbContext.PatientDb
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(patient =>
                    patient
                        .SetProperty(x => x.FirstName, model.FirstName)
                        .SetProperty(x => x.LastName, model.LastName)
                        .SetProperty(x => x.FavoriteColorId, model.FavoriteColorId)
                        .SetProperty(x => x.Gender, gender)
                        .SetProperty(x => x.UpdatedAt, DateTime.UtcNow)
                    ,cancellationToken
                );
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await dbContext.PatientDb
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
