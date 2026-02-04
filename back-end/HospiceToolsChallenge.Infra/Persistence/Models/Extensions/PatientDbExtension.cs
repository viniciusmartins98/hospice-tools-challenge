using HospiceToolsChallenge.Domain.Entities;
using HospiceToolsChallenge.Domain.Enums;
using HospiceToolsChallenge.Infra.Entities;

namespace HospiceToolsChallenge.Infra.Persistence.Models.Extensions
{
    public static class PatientDbExtension
    {
        public static Patient MapToEntity(this PatientDb patient)
        {
            if (patient == null) {
                return null;
            }

            GenderEnum? gender = null;
            if (Enum.TryParse(patient.Gender, out GenderEnum result))
            {
                gender = result;
            }

            return new Patient
            {
                Age = patient.Age,
                CreatedAt = patient.CreatedAt,
                FavoriteColor = patient.FavoriteColor != null ? new Color
                {
                    Id = patient.FavoriteColor.Id,
                    Name = patient.FavoriteColor.Name,
                    HexCode = patient.FavoriteColor.HexCode,
                } : null,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Gender = gender,
                Id = patient.Id,
                UpdatedAt = patient.UpdatedAt
            };
        }

        public static PatientDb MapToDb(this Patient patient)
        {
            if (patient == null)
            {
                return null;
            }

            return new PatientDb
            {
                Id = patient.Id,
                Age = patient.Age,
                CreatedAt = patient.CreatedAt,
                UpdatedAt = patient.UpdatedAt,
                FavoriteColor = patient.FavoriteColor != null ? new ColorDb
                {
                    Id = patient.FavoriteColor.Id,
                    Name = patient.FavoriteColor.Name,
                    HexCode = patient.FavoriteColor.HexCode,
                } : null,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Gender = patient.Gender?.ToString(),
                FavoriteColorId = patient.FavoriteColor?.Id,
            };
        }
    }
}
