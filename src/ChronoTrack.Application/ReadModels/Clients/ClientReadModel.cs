namespace ChronoTrack.Application.ReadModels.Clients
{
    public sealed class ClientReadModel
    {
        public int Id { get; init; }
        public int WorkspaceId { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}