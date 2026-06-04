namespace ChronoTrack.Application.ReadModels.Clients
{
    public sealed class ClientReadModel
    {
        public int Id { get; init; }
        public int WorkspaceId { get; init; }
        public string Name { get; init; } = string.Empty;
        public DateTimeOffset CreatedAt { get; init; }
        public string CreatedBy { get; init; } = string.Empty;
        public DateTimeOffset? UpdatedAt { get; init; }
        public string? UpdatedBy { get; init; }
    }
}
