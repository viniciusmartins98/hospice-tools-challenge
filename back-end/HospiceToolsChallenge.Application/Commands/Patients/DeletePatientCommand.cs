using MediatR;

namespace HospiceToolsChallenge.Application.Commands.Patients
{
    public class DeletePatientCommand : IRequest
    {
        public Guid PatientId { get; set; }
    }
}
