using HospiceToolsChallenge.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace HospiceToolsChallenge.Infra.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PaginatedResult<TData>> ToPagedResultAsync<TData, TFilter>(
            this IQueryable<TData> query, PaginatedFilter<TFilter> filter,
            CancellationToken cancellationToken = default)
        {
            if (filter.PageSize <= 0)
            {
                throw new ArgumentException("Page size must be greater than zero");
            }

            var totalItens = await query.CountAsync(cancellationToken);

            var pageResults = await query.Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<TData>
            {
                Data = pageResults,
                CurrentPage = filter.Page,
                PageSize = filter.PageSize,
                TotalItens = totalItens,
                TotalPages = (int)Math.Ceiling(totalItens / (decimal)filter.PageSize)
            };
        }
    }
}
