namespace ChronoTrack.Application.ReadModels.Workspaces
{
    public sealed record WorkspaceReadModel(
        int Id,
        string Name,
        string? Description);
}