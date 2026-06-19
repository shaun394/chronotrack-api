using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Application.ReadModels.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntries.Queries.ListByProject
{
    public sealed class ListTimeEntriesByProjectQueryHandler
        : IRequestHandler<ListTimeEntriesByProjectQuery, IReadOnlyCollection<TimeEntryReadModel>>
    {
        private readonly ITimeEntryReadRepository _timeEntryReadRepository;

        public ListTimeEntriesByProjectQueryHandler(ITimeEntryReadRepository timeEntryReadRepository)
        {
            _timeEntryReadRepository = timeEntryReadRepository;
        }

        public async Task<IReadOnlyCollection<TimeEntryReadModel>> Handle(
            ListTimeEntriesByProjectQuery query,
            CancellationToken ct)
        {
            return await _timeEntryReadRepository.ListByProjectAsync(query.ProjectId, ct);
        }
    }
}