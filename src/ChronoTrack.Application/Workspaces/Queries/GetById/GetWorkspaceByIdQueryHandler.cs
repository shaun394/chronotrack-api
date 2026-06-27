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
        private readonly IWorkspaceReadRepository _repository;

        public GetWorkspaceByIdQueryHandler(
            IWorkspaceReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<WorkspaceReadModel> Handle(
            GetWorkspaceByIdQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .GetByIdAsync(query.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Workspace),
                    query.Id,
                    nameof(GetWorkspaceByIdQueryHandler));
            }

            return result;
        }
    }
}