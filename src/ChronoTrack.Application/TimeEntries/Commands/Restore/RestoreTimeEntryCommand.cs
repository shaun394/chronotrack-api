using MediatR;

namespace ChronoTrack.Application.TimeEntries.Commands.Restore
{
    public sealed record RestoreTimeEntryCommand(
        int Id,
        string Actor) : IRequest<int>;
}