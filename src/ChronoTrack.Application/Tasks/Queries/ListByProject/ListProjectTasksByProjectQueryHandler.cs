using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using ChronoTrack.Application.ReadModels.Tasks;
using MediatR;

namespace ChronoTrack.Application.Tasks.Queries.ListByProject
{
    public sealed class ListProjectTasksByProjectQueryHandler
        : IRequestHandler<
            ListProjectTasksByProjectQuery,
            IReadOnlyCollection<ProjectTaskReadModel>>
    {
        private readonly IProjectTaskReadRepository _repository;

        public ListProjectTasksByProjectQueryHandler(
            IProjectTaskReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<ProjectTaskReadModel>> Handle(
            ListProjectTasksByProjectQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .ListByProjectAsync(query.ProjectId, ct);

            return result;
        }
    }
}