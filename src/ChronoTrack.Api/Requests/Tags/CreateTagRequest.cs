namespace ChronoTrack.Api.Requests.Tags
{
    public sealed record CreateTagRequest(
        int WorkspaceId,
        string Name);
}