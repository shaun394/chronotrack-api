namespace ChronoTrack.Domain.Common.Exceptions
{
    public sealed class DomainValidationException : DomainExceptionBase
    {
        public DomainValidationException(
            string entity,
            string message)
            : base(entity, message)
        {
        }
    }
}