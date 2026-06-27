using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Restore
{
    public sealed class RestoreWorkspaceCommandHandler
        : IRequestHandler<RestoreWorkspaceCommand, int>
    {
        private readonly IWorkspaceWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreWorkspaceCommandHandler(
            IWorkspaceWriteRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RestoreWorkspaceCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Workspace),
                    command.Id,
                    nameof(RestoreWorkspaceCommandHandler));
            }

            result.Restore(command.Actor, DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return result.Id;
        }
    }
}