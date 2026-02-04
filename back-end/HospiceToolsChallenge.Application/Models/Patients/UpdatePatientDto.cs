using HospiceToolsChallenge.Domain.Enums;

namespace HospiceToolsChallenge.Application.Models.Patients
{
    public class UpdatePatientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum? Gender { get; set; }
        public Guid? FavoriteColorId { get; set; }
    }
}
