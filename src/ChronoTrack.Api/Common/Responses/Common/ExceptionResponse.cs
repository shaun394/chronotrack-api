namespace ChronoTrack.Api.Common.Responses.Common
{
    public sealed class ExceptionResponse
    {
        public ExceptionResponse(
            int statusCode,
            string message,
            string? reasonCode = null,
            IEnumerable<string>? errors = null)
        {
            StatusCode = statusCode;
            Message = message;
            ReasonCode = reasonCode;
            Errors = errors;
        }

        public int StatusCode { get; }
        public string Message { get; }
        public string? ReasonCode { get; }
        public IEnumerable<string>? Errors { get; }
    }
}