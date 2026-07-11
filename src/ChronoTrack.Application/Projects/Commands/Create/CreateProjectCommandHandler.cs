using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Clients;
using ChronoTrack.Domain.Events.Project;
using ChronoTrack.Domain.Projects;
using ChronoTrack.Domain.Workspaces;
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
        private readonly IEventStore _eventStore;

        public CreateProjectCommandHandler(
            IProjectWriteRepository projectWriteRepository,
            IWorkspaceReadRepository workspaceReadRepository,
            IClientReadRepository clientReadRepository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _projectWriteRepository = projectWriteRepository;
            _workspaceReadRepository = workspaceReadRepository;
            _clientReadRepository = clientReadRepository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            CreateProjectCommand command,
            CancellationToken ct)
        {
            var workspace = await _workspaceReadRepository
                .GetByIdAsync(command.WorkspaceId, ct);

            if (workspace is null)
            {
                throw new NotFoundException(
                    nameof(Workspace),
                    command.WorkspaceId,
                    nameof(CreateProjectCommandHandler));
            }

            if (command.ClientId.HasValue)
            {
                var client = await _clientReadRepository
                    .GetByIdAsync(command.ClientId.Value, ct);

                if (client is null)
                {
                    throw new NotFoundException(
                        nameof(Client),
                        command.ClientId.Value,
                        nameof(CreateProjectCommandHandler));
                }
            }

            var result = Project.Create(
                command.WorkspaceId,
                command.ClientId,
                command.Name,
                command.Description,
                command.IsBillable,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _projectWriteRepository.AddAsync(result, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new ProjectCreated
                {
                    ProjectId = result.Id,
                    WorkspaceId = result.WorkspaceId,
                    ClientId = result.ClientId,
                    Name = result.Name,
                    Description = result.Description,
                    IsBillable = result.IsBillable,
                    Actor = command.Actor,
                    OccurredAt = result.CreatedAt
                },
                ct);

            return result.Id;
        }
    }
}