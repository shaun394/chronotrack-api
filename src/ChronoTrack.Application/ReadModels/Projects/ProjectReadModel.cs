namespace ChronoTrack.Application.ReadModels.Projects
{
    public sealed class ProjectReadModel
    {
        public int Id { get; init; }
        public int WorkspaceId { get; init; }
        public int? ClientId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public bool IsBillable { get; init; }
    }
}