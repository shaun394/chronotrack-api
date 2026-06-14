namespace ChronoTrack.Api.Requests.Tasks
{
    public sealed record UpdateProjectTaskRequest(
        string Name,
        string? Description,
        bool IsBillable);
}