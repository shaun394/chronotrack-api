namespace ChronoTrack.Api.Requests.Tasks
{
    public sealed record CreateProjectTaskRequest(
        int ProjectId,
        string Name,
        string? Description,
        bool IsBillable);
}