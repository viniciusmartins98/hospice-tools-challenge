using HospiceToolsChallenge.Application.Models.Colors;
using HospiceToolsChallenge.Domain.Enums;

namespace HospiceToolsChallenge.Application.Models.Patients
{
    public class PatientModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public GenderEnum? Gender { get; set; }

        public int? Age { get; set; }

        public ColorModel FavoriteColor { get; set; } = null;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
