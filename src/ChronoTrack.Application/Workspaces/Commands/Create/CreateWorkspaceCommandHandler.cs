using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Create
{
    public sealed class CreateWorkspaceCommandHandler
        : IRequestHandler<CreateWorkspaceCommand, int>
    {
        private readonly IWorkspaceWriteRepository _workspaceWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkspaceCommandHandler(
            IWorkspaceWriteRepository workspaceWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _workspaceWriteRepository = workspaceWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            CreateWorkspaceCommand command,
            CancellationToken ct)
        {
            var workspace = Workspace.Create(
                command.Name,
                command.Description,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _workspaceWriteRepository.AddAsync(workspace, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return workspace.Id;
        }
    }
}