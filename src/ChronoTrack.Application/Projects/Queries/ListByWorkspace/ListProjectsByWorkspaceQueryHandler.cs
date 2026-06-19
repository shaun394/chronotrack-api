using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Application.ReadModels.Projects;
using MediatR;

namespace ChronoTrack.Application.Projects.Queries.ListByWorkspace
{
    public sealed class ListProjectsByWorkspaceQueryHandler
        : IRequestHandler<
            ListProjectsByWorkspaceQuery,
            IReadOnlyCollection<ProjectReadModel>>
    {
        private readonly IProjectReadRepository _projectReadRepository;

        public ListProjectsByWorkspaceQueryHandler(
            IProjectReadRepository projectReadRepository)
        {
            _projectReadRepository = projectReadRepository;
        }

        public async Task<IReadOnlyCollection<ProjectReadModel>> Handle(
            ListProjectsByWorkspaceQuery query,
            CancellationToken ct)
        {
            return await _projectReadRepository
                .ListByWorkspaceAsync(query.WorkspaceId, ct);
        }
    }
}