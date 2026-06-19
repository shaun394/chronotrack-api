using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Projects;
using MediatR;

namespace ChronoTrack.Application.Projects.Commands.Create
{
    public sealed class CreateProjectCommandHandler
        : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectWriteRepository _projectWriteRepository;
        private readonly IWorkspaceReadRepository _workspaceReadRepository;
        private readonly IClientReadRepository _clientReadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectCommandHandler(
            IProjectWriteRepository projectWriteRepository,
            IWorkspaceReadRepository workspaceReadRepository,
            IClientReadRepository clientReadRepository,
            IUnitOfWork unitOfWork)
        {
            _projectWriteRepository = projectWriteRepository;
            _workspaceReadRepository = workspaceReadRepository;
            _clientReadRepository = clientReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            CreateProjectCommand command,
            CancellationToken ct)
        {
            var workspace = await _workspaceReadRepository
                .GetByIdAsync(command.WorkspaceId, ct);

            if (workspace is null)
                throw new NotFoundException("Workspace was not found.");

            if (command.ClientId.HasValue)
            {
                var client = await _clientReadRepository
                    .GetByIdAsync(command.ClientId.Value, ct);

                if (client is null)
                    throw new NotFoundException("Client was not found.");

                if (client.WorkspaceId != command.WorkspaceId)
                    throw new NotFoundException("Client was not found in the selected workspace");
            }

            var project = Project.Create(
                command.WorkspaceId,
                command.ClientId,
                command.Name,
                command.Description,
                command.IsBillable,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _projectWriteRepository.AddAsync(project, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return project.Id;
        }
    }
}
