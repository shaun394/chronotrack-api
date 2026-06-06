using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using MediatR;

namespace ChronoTrack.Application.Projects.Commands.Update
{
    public sealed class UpdateProjectCommandHandler
        : IRequestHandler<UpdateProjectCommand, int>
    {
        private readonly IProjectWriteRepository _projectWriteRepository;
        private readonly IClientReadRepository _clientReadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectCommandHandler(
            IProjectWriteRepository projectWriteRepository,
            IClientReadRepository clientReadRepository,
            IUnitOfWork unitOfWork)
        {
            _projectWriteRepository = projectWriteRepository;
            _clientReadRepository = clientReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            UpdateProjectCommand command,
            CancellationToken ct)
        {
            var project = await _projectWriteRepository.GetForUpdateAsync(command.Id, ct);

            if (project is null)
                throw new NotFoundException("Project was not found.");

            if (command.ClientId.HasValue)
            {
                var client = await _clientReadRepository.GetByIdAsync(command.ClientId.Value, ct);

                if (client is null)
                    throw new NotFoundException("Client was not found.");

                if (client.WorkspaceId != project.WorkspaceId)
                    throw new NotFoundException("Client was not found in the selected workspace.");
            }

            project.Update(
                command.ClientId,
                command.Name,
                command.Description,
                command.IsBillable,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return project.Id;
        }
    }
}