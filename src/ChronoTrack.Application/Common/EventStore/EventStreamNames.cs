namespace ChronoTrack.Application.Common.EventStore
{
    public static class EventStreamNames
    {
        public static string Workspace(int workspaceId)
        {
            return $"Workspace:{workspaceId}";
        }
    }
}