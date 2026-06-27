using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Application.ReadModels.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntries.Queries.ListByProject
{
    public sealed class ListTimeEntriesByProjectQueryHandler
        : IRequestHandler<
            ListTimeEntriesByProjectQuery,
            IReadOnlyCollection<TimeEntryReadModel>>
    {
        private readonly ITimeEntryReadRepository _repository;

        public ListTimeEntriesByProjectQueryHandler(
            ITimeEntryReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<TimeEntryReadModel>> Handle(
            ListTimeEntriesByProjectQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .ListByProjectAsync(query.ProjectId, ct);

            return result;
        }
    }
}