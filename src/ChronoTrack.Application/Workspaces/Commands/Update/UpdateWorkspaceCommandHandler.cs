using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Update
{
    public sealed class UpdateWorkspaceCommandHandler
        : IRequestHandler<UpdateWorkspaceCommand, int>
    {
        private readonly IWorkspaceWriteRepository _workspaceWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkspaceCommandHandler(
            IWorkspaceWriteRepository workspaceWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _workspaceWriteRepository = workspaceWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            UpdateWorkspaceCommand command,
            CancellationToken ct)
        {
            var workspace = await _workspaceWriteRepository
                .GetForUpdateAsync(command.Id, ct);

            if (workspace is null)
            {
                throw new NotFoundException("Workspace was not found.");
            }

            workspace.Update(
                command.Name,
                command.Description,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return workspace.Id;
        }
    }
}