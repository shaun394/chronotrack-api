namespace ChronoTrack.Api.Responses
{
    public sealed class ApiError
    {
        public string? Field { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}