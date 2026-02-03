using MediatR;
using HospiceToolsChallenge.Application.Models.Users;

namespace HospiceToolsChallenge.Application.Queries.Auth
{
    public class GetUserByCredentialsQuery : IRequest<UserModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
