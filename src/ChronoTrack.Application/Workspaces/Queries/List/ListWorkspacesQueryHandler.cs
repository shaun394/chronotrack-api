using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Application.ReadModels.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Queries.List
{
    public sealed class ListWorkspacesQueryHandler
        : IRequestHandler<
            ListWorkspacesQuery,
            IReadOnlyCollection<WorkspaceReadModel>>
    {
        private readonly IWorkspaceReadRepository _repository;

        public ListWorkspacesQueryHandler(
            IWorkspaceReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<WorkspaceReadModel>> Handle(
            ListWorkspacesQuery request,
            CancellationToken ct)
        {
            var result = await _repository
                .ListAsync(ct);

            return result;
        }
    }
}