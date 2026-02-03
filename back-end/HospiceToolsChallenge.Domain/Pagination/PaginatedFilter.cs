namespace HospiceToolsChallenge.Domain.Pagination {
    public class PaginatedFilter<T> {
        public int Page { get; set; }

        public int PageSize { get; set; } = 10;

        public T Filter { get; set; }
    }
}
