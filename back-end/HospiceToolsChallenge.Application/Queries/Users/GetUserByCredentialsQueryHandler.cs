using MediatR;
using HospiceToolsChallenge.Application.Models.Users;
using HospiceToolsChallenge.Application.Repositories;
using HospiceToolsChallenge.Domain.Entities;

namespace HospiceToolsChallenge.Application.Queries.Auth
{
    public class GetUserByCredentialsQueryHandler(IUserRepository _repository) : IRequestHandler<GetUserByCredentialsQuery, UserModel>
    {
        public async Task<UserModel> Handle(GetUserByCredentialsQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByCredentials(request.Username, request.Password);
            return MapUserModel(user);
        }

        private UserModel MapUserModel(User user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserModel
            {
                Name = user.Name,
                Username = user.Username
            };
        }
    }
}
