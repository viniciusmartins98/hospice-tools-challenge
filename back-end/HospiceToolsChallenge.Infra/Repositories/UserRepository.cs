using HospiceToolsChallenge.Application.Repositories;
using HospiceToolsChallenge.Domain.Entities;

namespace HospiceToolsChallenge.Infra.Repositories
{
    internal class UserRepository : IUserRepository
    {
        public async Task<User> GetByCredentials(string email, string password)
        {
            return _mockedUser.SingleOrDefault(x => x.Username == email && x.Password == password);
        }

        private IEnumerable<User> _mockedUser = [
            new User { Name = "Admin", Username = "admin", Password = "test123" },
        ];
    }
}
