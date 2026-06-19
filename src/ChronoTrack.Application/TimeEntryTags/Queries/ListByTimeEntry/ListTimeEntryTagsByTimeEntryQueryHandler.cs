using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Application.Interfaces.Repositories.TimeEntryTags;
using ChronoTrack.Application.ReadModels.TimeEntryTags;
using ChronoTrack.Domain.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntryTags.Queries.ListByTimeEntry
{
    public sealed class ListTimeEntryTagsByTimeEntryQueryHandler
        : IRequestHandler<ListTimeEntryTagsByTimeEntryQuery, IReadOnlyCollection<TimeEntryTagReadModel>>
    {
        private readonly ITimeEntryTagReadRepository _timeEntryTagReadRepository;
        private readonly ITimeEntryReadRepository _timeEntryReadRepository;

        public ListTimeEntryTagsByTimeEntryQueryHandler(
            ITimeEntryTagReadRepository timeEntryTagReadRepository,
            ITimeEntryReadRepository timeEntryReadRepository)
        {
            _timeEntryTagReadRepository = timeEntryTagReadRepository;
            _timeEntryReadRepository = timeEntryReadRepository;
        }

        public async Task<IReadOnlyCollection<TimeEntryTagReadModel>> Handle(
            ListTimeEntryTagsByTimeEntryQuery query,
            CancellationToken ct)
        {
            var timeEntry = await _timeEntryReadRepository
                .GetByIdAsync(query.TimeEntryId, ct);

            if (timeEntry is null)
            {
                throw new NotFoundException(
                    nameof(TimeEntry),
                    query.TimeEntryId,
                    nameof(ListTimeEntryTagsByTimeEntryQueryHandler));
            }

            var result = await _timeEntryTagReadRepository
                .ListByTimeEntryAsync(
                    query.TimeEntryId,
                    ct);

            return result;
        }
    }
}