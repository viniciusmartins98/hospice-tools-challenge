using HospiceToolsChallenge.Application.Models.Colors;
using HospiceToolsChallenge.Application.Repositories;
using MediatR;

namespace HospiceToolsChallenge.Application.Queries.Colors
{
    public class ListColorsQueryHandler(IColorRepository repository) : IRequestHandler<ListColorsQuery, IEnumerable<ColorModel>>
    {
        public async Task<IEnumerable<ColorModel>> Handle(ListColorsQuery request, CancellationToken cancellationToken)
        {
            var colors = await repository.ListAsync(cancellationToken);
            return colors.Select(x => new ColorModel
            {
                HexCode = x.HexCode,
                Id = x.Id,
                Name = x.Name
            });
        }
    }
}
