namespace ChronoTrack.Application.Common.Models
{
    public sealed class PagedResult<T>
    {
        public PagedResult(
            IReadOnlyCollection<T> items,
            int page,
            int pageSize,
            int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public IReadOnlyCollection<T> Items { get; }

        public int Page { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public bool HasPreviousPage => Page > 1;

        public bool HasNextPage => Page < TotalPages;
    }
}