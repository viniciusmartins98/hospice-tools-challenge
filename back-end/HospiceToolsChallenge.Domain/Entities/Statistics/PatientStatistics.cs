namespace HospiceToolsChallenge.Domain.Entities.Statistics
{
    public class PatientStatistics
    {
        public int PatientsCount { get; set; }

        public int MaleCount { get; set; }

        public int FemaleCount { get; set; }

        public int PatientsWithColorCount { get; set; }

        public int PatientsWithNoColorsCount { get; set; }

        public PatientCountByColor[] PatientsCountByColor { get; set; } = [];

        public PatientCountByColorAndGender[] PatientsCountByColorAndGender { get; set; } = [];

        public PatientFavoriteColorCountByAgeRange[] FavoriteColorPatientsCountByAgeRange { get; set; }

        public AgeRange[] GetAllAgeRanges()
        {
            return [
                new AgeRange(0, 18),
                new AgeRange(19, 39),
                new AgeRange(40, 64),
                new AgeRange(65, 74),
                new AgeRange(75, 84),
                new AgeRange(85, null)
            ];
        }
    }
}
