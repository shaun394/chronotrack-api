namespace ChronoTrack.Api.Requests.Clients
{
    public sealed record CreateClientRequest(
        int WorkspaceId,
        string Name);
}