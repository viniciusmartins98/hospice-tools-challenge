namespace HospiceToolsChallenge.Domain.Entities.Statistics
{
    public class PatientFavoriteColorCountByAgeRange
    {
        public int PatientsCount { get; set; }

        public AgeRange AgeRange { get; set; }

        public Color FavoriteColor { get; set; }
    }
}
