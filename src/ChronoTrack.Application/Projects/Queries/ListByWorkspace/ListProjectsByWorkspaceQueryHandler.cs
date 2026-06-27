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
        private readonly IProjectReadRepository _repository;

        public ListProjectsByWorkspaceQueryHandler(
            IProjectReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<ProjectReadModel>> Handle(
            ListProjectsByWorkspaceQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .ListByWorkspaceAsync(query.WorkspaceId, ct);

            return result;
        }
    }
}