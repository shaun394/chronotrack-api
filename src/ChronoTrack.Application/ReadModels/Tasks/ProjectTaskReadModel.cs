namespace ChronoTrack.Application.ReadModels.Tasks
{
    public sealed class ProjectTaskReadModel
    {
        public int Id { get; init; }
        public int ProjectId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public bool IsBillable { get; init; }
    }
}