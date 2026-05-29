using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Application.ReadModels.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Queries.GetById
{
    public sealed class GetWorkspaceByIdQueryHandler : IRequestHandler<GetWorkspaceByIdQuery, WorkspaceReadModel>
    {
        private readonly IWorkspaceReadRepository _workspaceReadRepository;

        public GetWorkspaceByIdQueryHandler(IWorkspaceReadRepository workspaceReadRepository)
        {
            _workspaceReadRepository = workspaceReadRepository;
        }

        public async Task<WorkspaceReadModel> Handle(GetWorkspaceByIdQuery request, CancellationToken ct)
        {
            var workspace = await _workspaceReadRepository.GetByIdAsync(request.Id, ct);

            if (workspace is null)
            {
                throw new NotFoundException("Workspace was not found.");
            }

            return workspace;
        }
    }
}