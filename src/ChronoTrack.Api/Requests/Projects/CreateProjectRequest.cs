namespace ChronoTrack.Api.Requests.Projects
{
    public sealed record CreateProjectRequest(
        int WorkspaceId,
        int? ClientId,
        string Name,
        string? Description,
        bool IsBillable);
}