using ChronoTrack.Application.ReadModels.TimeEntryTags;
using MediatR;

namespace ChronoTrack.Application.TimeEntryTags.Queries.ListByTimeEntry
{
    public sealed record ListTimeEntryTagsByTimeEntryQuery(
        int TimeEntryId) : IRequest<IReadOnlyCollection<TimeEntryTagReadModel>>;
}