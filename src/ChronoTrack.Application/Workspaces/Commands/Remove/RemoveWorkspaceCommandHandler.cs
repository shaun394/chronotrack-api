using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Remove
{
    public sealed class RemoveWorkspaceCommandHandler
        : IRequestHandler<RemoveWorkspaceCommand, int>
    {
        private readonly IWorkspaceWriteRepository _workspaceWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveWorkspaceCommandHandler(
            IWorkspaceWriteRepository workspaceWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _workspaceWriteRepository = workspaceWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RemoveWorkspaceCommand request,
            CancellationToken ct)
        {
            var workspace = await _workspaceWriteRepository
                .GetForUpdateAsync(request.Id, ct);

            if (workspace is null)
            {
                throw new NotFoundException("Workspace was not found.");
            }

            workspace.Remove(request.Actor, DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return workspace.Id;
        }
    }
}