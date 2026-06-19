namespace ChronoTrack.Application.Common.Exceptions
{
    public sealed class NotFoundException : ApplicationExceptionBase
    {
        public NotFoundException(
            string entity,
            object? key = null,
            string? sourceContext = null)
            : base(BuildMessage(entity, key))
        {
            Entity = entity;
            Key = key?.ToString();
            SourceContext = sourceContext;
        }

        public string Entity { get; }

        public string? Key { get; }

        public string? SourceContext { get; }

        private static string BuildMessage(string entity, object? key)
        {
            return key is null
                ? $"{entity} was not found."
                : $"{entity} with key '{key}' was not found.";
        }
    }
}