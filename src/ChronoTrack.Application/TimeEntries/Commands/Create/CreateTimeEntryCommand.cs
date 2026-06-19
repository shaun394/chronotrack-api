using MediatR;

namespace ChronoTrack.Application.TimeEntries.Commands.Create
{
    public sealed record CreateTimeEntryCommand(
        int WorkspaceId,
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