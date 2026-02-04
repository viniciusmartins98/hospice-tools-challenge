using HospiceToolsChallenge.Domain.Entities;

namespace HospiceToolsChallenge.Application.Repositories
{
    public interface IColorRepository
    {
        Task<IEnumerable<Color>> ListAsync(CancellationToken cancellationToken);
    }
}
