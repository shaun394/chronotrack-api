namespace ChronoTrack.Api.Responses
{
    public sealed class PagedApiResponse<T>
    {
        private PagedApiResponse(
            bool success,
            IReadOnlyCollection<T> data,
            int page,
            int pageSize,
            int totalCount,
            string? error)
        {
            Success = success;
            Data = data;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            Error = error;
        }

        public bool Success { get; }

        public IReadOnlyCollection<T> Data { get; }

        public int Page { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public string? Error { get; }

        public static PagedApiResponse<T> Ok(
            IReadOnlyCollection<T> data,
            int page,
            int pageSize,
            int totalCount)
        {
            return new PagedApiResponse<T>(
                true,
                data,
                page,
                pageSize,
                totalCount,
                null);
        }

        public static PagedApiResponse<T> Fail(string error)
        {
            return new PagedApiResponse<T>(
                false,
                Array.Empty<T>(),
                1,
                0,
                0,
                error);
        }
    }
}