using HospiceToolsChallenge.Domain.Enums;

namespace HospiceToolsChallenge.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FullName => FirstName + " " + LastName;

        public GenderEnum? Gender { get; set; }

        public int? Age { get; set; }

        public Color FavoriteColor { get; set; } = null;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
