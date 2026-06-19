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
        private readonly IWorkspaceReadRepository _workspaceReadRepository;

        public ListWorkspacesQueryHandler(
            IWorkspaceReadRepository workspaceReadRepository)
        {
            _workspaceReadRepository = workspaceReadRepository;
        }

        public async Task<IReadOnlyCollection<WorkspaceReadModel>> Handle(
            ListWorkspacesQuery request,
            CancellationToken ct)
        {
            return await _workspaceReadRepository
                .ListAsync(ct);
        }
    }
}