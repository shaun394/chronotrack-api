namespace ChronoTrack.Api.Requests.Projects
{
    public sealed record UpdateProjectRequest(
        int? ClientId,
        string Name,
        string? Description,
        bool IsBillable);
}