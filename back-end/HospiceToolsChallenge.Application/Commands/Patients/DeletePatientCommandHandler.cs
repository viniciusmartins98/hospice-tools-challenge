using HospiceToolsChallenge.Application.Repositories;
using MediatR;

namespace HospiceToolsChallenge.Application.Commands.Patients
{
    public class DeletePatientCommandHandler(IPatientRepository repository) : IRequestHandler<DeletePatientCommand>
    {
        public async Task Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(request.PatientId, cancellationToken);
        }
    }
}
