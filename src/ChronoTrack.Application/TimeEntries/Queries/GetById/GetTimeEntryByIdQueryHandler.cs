using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Application.ReadModels.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntries.Queries.GetById
{
    public sealed class GetTimeEntryByIdQueryHandler
        : IRequestHandler<
            GetTimeEntryByIdQuery,
            TimeEntryReadModel>
    {
        private readonly ITimeEntryReadRepository _timeEntryReadRepository;

        public GetTimeEntryByIdQueryHandler(
            ITimeEntryReadRepository timeEntryReadRepository)
        {
            _timeEntryReadRepository = timeEntryReadRepository;
        }

        public async Task<TimeEntryReadModel> Handle(
            GetTimeEntryByIdQuery query,
            CancellationToken ct)
        {
            var timeEntry = await _timeEntryReadRepository
                .GetByIdAsync(query.Id, ct);

            if (timeEntry is null)
            {
                throw new NotFoundException("Time entry was not found.");
            }

            return timeEntry;
        }
    }
}