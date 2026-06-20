namespace ChronoTrack.Api.Responses
{
    public sealed class PagedApiResponse<T>
    {
        private PagedApiResponse(
            bool success,
            string? message,
            IReadOnlyCollection<T> data,
            int pageNumber,
            int pageSize,
            int totalCount,
            IReadOnlyCollection<ApiError> errors)
        {
            Success = success;
            Message = message;
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Errors = errors;
        }

        public bool Success { get; }
        public string? Message { get; }
        public IReadOnlyCollection<T> Data { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public IReadOnlyCollection<ApiError> Errors { get; }

        public static PagedApiResponse<T> Ok(
            IReadOnlyCollection<T> data,
            int pageNumber,
            int pageSize,
            int totalCount)
        {
            return new PagedApiResponse<T>(
                true,
                null,
                data,
                pageNumber,
                pageSize,
                totalCount,
                Array.Empty<ApiError>());
        }
    }
}