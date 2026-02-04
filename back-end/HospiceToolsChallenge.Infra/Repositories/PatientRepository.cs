using HospiceToolsChallenge.Application.Models.Patients;
using HospiceToolsChallenge.Application.Repositories;
using HospiceToolsChallenge.Domain.Entities;
using HospiceToolsChallenge.Domain.Entities.Statistics;
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
                    , cancellationToken
                );
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await dbContext.PatientDb
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<PatientStatistics> GetPatientStatisticsAsync(CancellationToken cancellationToken)
        {
            PatientStatistics patientStatistics = new();

            // List patients age and favorite color only
            var patientAgeAndFavoriteColors = await dbContext.PatientDb
                .Select(p => new { p.Age, p.FavoriteColor })
                .ToListAsync(cancellationToken);

            // Get patients count grouped by color
            var patientsCountByColor = patientAgeAndFavoriteColors
                .Where(x => x.FavoriteColor != null)
                .GroupBy(p => new { p.FavoriteColor.Id, p.FavoriteColor })
                .Select(g => new PatientCountByColor
                {
                    Color = new Color
                    {
                        Id = g.Key.FavoriteColor.Id,
                        HexCode = g.Key.FavoriteColor.HexCode,
                        Name = g.Key.FavoriteColor.Name,
                    },
                    PatientsCount = g.Count()
                })
                .OrderByDescending(x => x.PatientsCount)
                .ToArray();

            // List patient by age ranges and count the favorite colors for each age range
            AgeRange[] ageRanges = patientStatistics.GetAllAgeRanges();
            var favoriteColorPatientCountByAgeRange = patientAgeAndFavoriteColors
                .Select(p => new
                {
                    p.FavoriteColor,
                    AgeRange = ageRanges.FirstOrDefault(r => p.Age >= r.From && (r.To == null || p.Age <= r.To))
                })
                .Where(x => x.AgeRange != null)
                .GroupBy(x => new { x.AgeRange, x.FavoriteColor.Id, x.FavoriteColor.Name, x.FavoriteColor.HexCode })
                .Select(g => new PatientFavoriteColorCountByAgeRange
                {
                    PatientsCount = g.Count(),
                    AgeRange = g.Key.AgeRange,
                    FavoriteColor = new Color { Id = g.Key.Id, Name = g.Key.Name, HexCode = g.Key.HexCode }
                })
                .OrderBy(x => x.AgeRange.From)
                .ThenByDescending(x => x.PatientsCount)
                .ToArray();

            patientStatistics.PatientsCount = patientAgeAndFavoriteColors.Count();
            patientStatistics.PatientsWithColorCount = patientAgeAndFavoriteColors.Where(x => x.FavoriteColor != null).Count();
            patientStatistics.PatientsWithNoColorsCount = patientAgeAndFavoriteColors.Where(x => x.FavoriteColor == null).Count();
            patientStatistics.PatientsCountByColor = patientsCountByColor;
            patientStatistics.FavoriteColorPatientsCountByAgeRange = favoriteColorPatientCountByAgeRange;
            return patientStatistics;
        }
    }
}
