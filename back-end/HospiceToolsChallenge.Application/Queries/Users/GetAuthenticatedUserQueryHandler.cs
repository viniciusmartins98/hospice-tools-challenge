using HospiceToolsChallenge.Application.Models.Users;
using HospiceToolsChallenge.Application.UserContext;
using MediatR;

namespace HospiceToolsChallenge.Application.Queries.Users
{
    public class GetAuthenticatedUserQueryHandler(IUserContext userContext) : IRequestHandler<GetAuthenticatedUserQuery, UserModel>
    {
        public Task<UserModel> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
        {
            if (userContext == null)  {
                return null;
            }

            return Task.FromResult(new UserModel
            {
                Name = userContext.Name,
                Username = userContext.Username
            });
        }
    }
}
