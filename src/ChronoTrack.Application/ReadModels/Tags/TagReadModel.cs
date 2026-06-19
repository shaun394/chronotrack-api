namespace ChronoTrack.Application.ReadModels.Tags
{
    public sealed class TagReadModel
    {
        public int Id { get; init; }
        public int WorkspaceId { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}