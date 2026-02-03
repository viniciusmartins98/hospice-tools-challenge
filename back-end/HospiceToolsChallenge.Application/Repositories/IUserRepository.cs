using HospiceToolsChallenge.Domain.Entities;

namespace HospiceToolsChallenge.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByCredentials(string email, string password);
    }
}
