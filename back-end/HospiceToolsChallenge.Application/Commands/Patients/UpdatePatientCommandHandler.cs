using HospiceToolsChallenge.Application.Repositories;
using MediatR;

namespace HospiceToolsChallenge.Application.Commands.Patients
{
    public class UpdatePatientCommandHandler(IPatientRepository repository) : IRequestHandler<UpdatePatientCommand>
    {
        public async Task Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            await repository.UpdateAsync(request.PatientId, request, cancellationToken);
        }
    }
}
