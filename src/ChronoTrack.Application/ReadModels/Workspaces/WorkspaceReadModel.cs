namespace ChronoTrack.Application.ReadModels.Workspaces
{
    public sealed class WorkspaceReadModel
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
    }
}