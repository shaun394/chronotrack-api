namespace ChronoTrack.Application.Common.Exceptions
{
    public sealed class DuplicateEntityException : ApplicationExceptionBase
    {
        public DuplicateEntityException(
            string entity,
            string? sourceContext = null)
            : base($"{entity} already exists.")
        {
            Entity = entity;
            SourceContext = sourceContext;
        }

        public string Entity { get; }

        public string? SourceContext { get; }
    }
}