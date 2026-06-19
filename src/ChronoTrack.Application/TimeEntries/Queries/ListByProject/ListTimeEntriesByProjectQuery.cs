using ChronoTrack.Application.ReadModels.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntries.Queries.ListByProject
{
    public sealed record ListTimeEntriesByProjectQuery(int ProjectId)
        : IRequest<IReadOnlyCollection<TimeEntryReadModel>>;
}