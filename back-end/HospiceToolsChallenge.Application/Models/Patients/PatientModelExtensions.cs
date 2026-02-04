using HospiceToolsChallenge.Application.Models.Colors;
using HospiceToolsChallenge.Domain.Entities;

namespace HospiceToolsChallenge.Application.Models.Patients
{
    public static class PatientModelExtensions
    {
        public static PatientModel ToModel(this Patient entity)
        {
            if (entity == null)
            {
                return null;
            }

            ColorModel favoriteColor = null;
            if (entity.FavoriteColor != null)
            {
                favoriteColor = new ColorModel
                {
                    HexCode = entity.FavoriteColor.HexCode,
                    Id = entity.FavoriteColor.Id,
                    Name = entity.FavoriteColor.Name
                };
            }


            return new PatientModel
            {
                UpdatedAt = entity.UpdatedAt,
                Age = entity.Age,
                CreatedAt = entity.CreatedAt,
                FavoriteColor = favoriteColor,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                FullName = entity.FullName,
                Gender = entity.Gender,
                Id = entity.Id,
            };
        }
    }
}
