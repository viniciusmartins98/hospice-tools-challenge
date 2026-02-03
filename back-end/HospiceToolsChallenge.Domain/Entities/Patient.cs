namespace HospiceToolsChallenge.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Gender { get; set; }

        public int? Age { get; set; }

        public Color FavoriteColor { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
