using HospiceToolsChallenge.Application.Models.Colors;
using MediatR;

namespace HospiceToolsChallenge.Application.Queries.Colors
{
    public class ListColorsQuery : IRequest<IEnumerable<ColorModel>>
    {
    }
}
