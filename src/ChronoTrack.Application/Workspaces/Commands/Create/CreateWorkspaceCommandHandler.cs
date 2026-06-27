using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Create
{
    public sealed class CreateWorkspaceCommandHandler
        : IRequestHandler<CreateWorkspaceCommand, int>
    {
        private readonly IWorkspaceWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkspaceCommandHandler(
            IWorkspaceWriteRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            CreateWorkspaceCommand command,
            CancellationToken ct)
        {
            var result = Workspace.Create(
                command.Name,
                command.Description,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _repository.AddAsync(result, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return result.Id;
        }
    }
}