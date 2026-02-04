using HospiceToolsChallenge.Domain.Entities;
using HospiceToolsChallenge.Infra.Entities;

namespace HospiceToolsChallenge.Infra.Persistence.Models.Extensions
{
    public static class ColorDbExtension
    {
        public static Color ToEntity(this ColorDb color)
        {
            if (color == null)
            {
                return null;
            }

            return new Color
            {
                HexCode = color.HexCode,
                Id = color.Id,
                Name = color.Name,
            };
        }
    }
}
