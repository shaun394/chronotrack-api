namespace ChronoTrack.Domain.Common.Exceptions
{
    public abstract class DomainExceptionBase : Exception
    {
        protected DomainExceptionBase(
            string entity,
            string message)
            : base($"[{entity}] {message}")
        {
            Entity = entity;
        }

        public string Entity { get; }
    }
}