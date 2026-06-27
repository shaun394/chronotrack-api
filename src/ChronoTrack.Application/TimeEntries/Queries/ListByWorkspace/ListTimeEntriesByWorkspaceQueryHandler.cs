using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Application.ReadModels.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntries.Queries.ListByWorkspace
{
    public sealed class ListTimeEntriesByWorkspaceQueryHandler
        : IRequestHandler<
            ListTimeEntriesByWorkspaceQuery,
            IReadOnlyCollection<TimeEntryReadModel>>
    {
        private readonly ITimeEntryReadRepository _repository;

        public ListTimeEntriesByWorkspaceQueryHandler(
            ITimeEntryReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<TimeEntryReadModel>> Handle(
            ListTimeEntriesByWorkspaceQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .ListByWorkspaceAsync(query.WorkspaceId, ct);

            return result;
        }
    }
}