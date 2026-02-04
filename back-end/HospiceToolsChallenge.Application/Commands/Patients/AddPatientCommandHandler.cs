using HospiceToolsChallenge.Application.Repositories;
using HospiceToolsChallenge.Domain.Entities;
using MediatR;

namespace HospiceToolsChallenge.Application.Commands.Patients
{
    public class AddPatientCommandHandler(IPatientRepository repository) : IRequestHandler<AddPatientCommand>
    {
        public async Task Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {
            var favoriteColor = request.FavoriteColorId != null ? new Color
            {
                Id = request.FavoriteColorId.Value
            } : null;

            await repository.AddAsync(new Patient
            {
                Id = Guid.NewGuid(),
                Age = request.Age,
                CreatedAt = DateTime.UtcNow,
                FirstName = request.FirstName,
                LastName = request.LastName,
                FavoriteColor = favoriteColor,
                Gender = request.Gender,
            }, cancellationToken);
        }
    }
}
