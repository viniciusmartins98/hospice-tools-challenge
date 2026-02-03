using HospiceToolsChallenge.Application.Models.Colors;

namespace HospiceToolsChallenge.Application.Models.Patients
{
    public class PatientModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Gender { get; set; }

        public int? Age { get; set; }

        public ColorModel FavoriteColor { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
