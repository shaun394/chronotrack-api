using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Application.ReadModels.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntries.Queries.ListByWorkspace
{
    public sealed class ListTimeEntriesByWorkspaceQueryHandler
        : IRequestHandler<ListTimeEntriesByWorkspaceQuery, IReadOnlyCollection<TimeEntryReadModel>>
    {
        private readonly ITimeEntryReadRepository _timeEntryReadRepository;

        public ListTimeEntriesByWorkspaceQueryHandler(ITimeEntryReadRepository timeEntryReadRepository)
        {
            _timeEntryReadRepository = timeEntryReadRepository;
        }

        public async Task<IReadOnlyCollection<TimeEntryReadModel>> Handle(
            ListTimeEntriesByWorkspaceQuery query,
            CancellationToken ct)
        {
            return await _timeEntryReadRepository.ListByWorkspaceAsync(query.WorkspaceId, ct);
        }
    }
}