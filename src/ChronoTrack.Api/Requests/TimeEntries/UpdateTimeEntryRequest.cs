namespace ChronoTrack.Api.Requests.TimeEntries
{
    public sealed record UpdateTimeEntryRequest(
        int ProjectId,
        int? ProjectTaskId,
        int? ClientId,
        string? Description,
        DateOnly WorkDate,
        TimeOnly StartTime,
        TimeOnly EndTime,
        bool IsBillable);
}