using MediatR;

namespace ChronoTrack.Application.TimeEntries.Commands.Update
{
    public sealed record UpdateTimeEntryCommand(
        int Id,
        int ProjectId,
        int? ProjectTaskId,
        int? ClientId,
        string? Description,
        DateOnly WorkDate,
        TimeOnly StartTime,
        TimeOnly EndTime,
        bool IsBillable,
        string Actor) : IRequest<int>;
}