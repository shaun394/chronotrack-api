namespace ChronoTrack.Api.Responses
{
    public sealed class ApiResponse<T>
    {
        private ApiResponse(bool success, T? data, string? error)
        {
            Success = success;
            Data = data;
            Error = error;
        }

        public bool Success { get; }

        public T? Data { get; }

        public string? Error { get; }

        public static ApiResponse<T> Ok(T data)
        {
            return new ApiResponse<T>(true, data, null);
        }

        public static ApiResponse<T> Fail(string error)
        {
            return new ApiResponse<T>(false, default, error);
        }
    }
}