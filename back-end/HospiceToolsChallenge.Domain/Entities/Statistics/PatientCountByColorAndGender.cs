using HospiceToolsChallenge.Domain.Enums;

namespace HospiceToolsChallenge.Domain.Entities.Statistics
{
    public class PatientCountByColorAndGender : PatientCountByColor
    {
        public GenderEnum Gender { get; set; }
    }
}
