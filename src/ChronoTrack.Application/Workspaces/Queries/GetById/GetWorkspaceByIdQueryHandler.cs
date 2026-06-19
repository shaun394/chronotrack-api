using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Application.ReadModels.Workspaces;
using ChronoTrack.Domain.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Queries.GetById
{
    public sealed class GetWorkspaceByIdQueryHandler
        : IRequestHandler<GetWorkspaceByIdQuery, WorkspaceReadModel>
    {
        private readonly IWorkspaceReadRepository _workspaceReadRepository;

        public GetWorkspaceByIdQueryHandler(
            IWorkspaceReadRepository workspaceReadRepository)
        {
            _workspaceReadRepository = workspaceReadRepository;
        }

        public async Task<WorkspaceReadModel> Handle(
            GetWorkspaceByIdQuery query,
            CancellationToken ct)
        {
            var workspace = await _workspaceReadRepository
                .GetByIdAsync(query.Id, ct);

            if (workspace is null)
            {
                throw new NotFoundException(
                    nameof(Workspace),
                    query.Id,
                    nameof(GetWorkspaceByIdQueryHandler));
            }

            return workspace;
        }
    }
}