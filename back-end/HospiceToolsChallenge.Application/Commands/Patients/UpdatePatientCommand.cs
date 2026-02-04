using HospiceToolsChallenge.Application.Models.Patients;
using MediatR;

namespace HospiceToolsChallenge.Application.Commands.Patients
{
    public class UpdatePatientCommand : UpdatePatientDto, IRequest
    {
        public Guid PatientId { get; set; }
    }
}
