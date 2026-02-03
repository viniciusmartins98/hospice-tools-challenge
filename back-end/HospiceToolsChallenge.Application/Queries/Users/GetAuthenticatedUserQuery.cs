using HospiceToolsChallenge.Application.Models.Users;
using MediatR;

namespace HospiceToolsChallenge.Application.Queries.Users
{
    public class GetAuthenticatedUserQuery : IRequest<UserModel>
    {
    }
}
