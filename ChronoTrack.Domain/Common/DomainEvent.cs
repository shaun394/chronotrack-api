namespace ChronoTrack.Domain.Common
{
    public abstract record DomainEvent
    {
        public required string Actor { get; init; }
        public required DateTimeOffset OccurredAt { get; init; }
    }
}
