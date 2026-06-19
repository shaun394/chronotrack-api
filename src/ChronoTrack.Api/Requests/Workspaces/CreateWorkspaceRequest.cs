namespace ChronoTrack.Api.Requests.Workspaces
{
    public sealed record CreateWorkspaceRequest(
        string Name,
        string? Description);
}