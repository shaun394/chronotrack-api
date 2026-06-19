using ChronoTrack.Application.ReadModels.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntries.Queries.ListByWorkspace
{
    public sealed record ListTimeEntriesByWorkspaceQuery(
        int WorkspaceId) : IRequest<IReadOnlyCollection<TimeEntryReadModel>>;
}