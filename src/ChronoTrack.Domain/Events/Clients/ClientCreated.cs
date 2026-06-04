using ChronoTrack.Domain.Common;

namespace ChronoTrack.Domain.Events.Clients
{
    public sealed record ClientCreated : DomainEvent
    {
        public required int ClientId { get; init; }
        public required int WorkspaceId { get; init; }
        public required string Name { get; init; }
    }
}