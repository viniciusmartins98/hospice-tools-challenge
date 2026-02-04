using HospiceToolsChallenge.Application.Repositories;
using HospiceToolsChallenge.Domain.Entities;
using HospiceToolsChallenge.Infra.Persistence;
using HospiceToolsChallenge.Infra.Persistence.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HospiceToolsChallenge.Infra.Repositories
{
    public class ColorRepository(DatabaseContext dbContext) : IColorRepository
    {
        public async Task<IEnumerable<Color>> ListAsync(CancellationToken cancellationToken)
        {
            return await dbContext.ColorDb
                .OrderBy(x => x.Name)
                .Select(x => x.ToEntity())
                .ToListAsync(cancellationToken);
        }
    }
}
