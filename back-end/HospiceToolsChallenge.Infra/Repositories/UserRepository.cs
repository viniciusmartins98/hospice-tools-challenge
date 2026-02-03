using HospiceToolsChallenge.Application.Repositories;
using HospiceToolsChallenge.Domain.Entities;

namespace HospiceToolsChallenge.Infra.Repositories
{
    internal class UserRepository : IUserRepository
    {
        public async Task<User> GetByCredentials(string username, string password)
        {
            return _mockedUser.SingleOrDefault(x => x.Username == username && x.Password == password);
        }

        private IEnumerable<User> _mockedUser = [
            new User { Name = "Admin", Username = "admin", Password = "admin" },
        ];
    }
}
