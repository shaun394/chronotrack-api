namespace ChronoTrack.Api.Requests.Workspaces
{
    public sealed record UpdateWorkspaceRequest(
        string Name,
        string? Description);
}