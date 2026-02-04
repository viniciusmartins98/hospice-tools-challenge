using HospiceToolsChallenge.Domain.Enums;
using MediatR;

namespace HospiceToolsChallenge.Application.Commands.Patients
{
    public class AddPatientCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public Guid? FavoriteColorId { get; set; }
        public GenderEnum? Gender { get; set; }
    }
}
