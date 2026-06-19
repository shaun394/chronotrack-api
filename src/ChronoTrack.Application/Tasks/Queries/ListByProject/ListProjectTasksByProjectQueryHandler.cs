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
        private readonly IProjectTaskReadRepository _projectTaskReadRepository;

        public ListProjectTasksByProjectQueryHandler(
            IProjectTaskReadRepository projectTaskReadRepository)
        {
            _projectTaskReadRepository = projectTaskReadRepository;
        }

        public async Task<IReadOnlyCollection<ProjectTaskReadModel>> Handle(
            ListProjectTasksByProjectQuery query,
            CancellationToken ct)
        {
            return await _projectTaskReadRepository
                .ListByProjectAsync(query.ProjectId, ct);
        }
    }
}