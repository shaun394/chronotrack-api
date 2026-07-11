using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Domain.Clients;
using ChronoTrack.Domain.Events.Project;
using ChronoTrack.Domain.Projects;
using MediatR;

namespace ChronoTrack.Application.Projects.Commands.Update
{
    public sealed class UpdateProjectCommandHandler
        : IRequestHandler<UpdateProjectCommand, int>
    {
        private readonly IProjectWriteRepository _projectWriteRepository;
        private readonly IClientReadRepository _clientReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public UpdateProjectCommandHandler(
            IProjectWriteRepository projectWriteRepository,
            IClientReadRepository clientReadRepository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _projectWriteRepository = projectWriteRepository;
            _clientReadRepository = clientReadRepository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            UpdateProjectCommand command,
            CancellationToken ct)
        {
            var result = await _projectWriteRepository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Project),
                    command.Id,
                    nameof(UpdateProjectCommandHandler));
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
                        nameof(UpdateProjectCommandHandler));
                }
            }

            result.Update(
                command.ClientId,
                command.Name,
                command.Description,
                command.IsBillable,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new ProjectUpdated
                {
                    ProjectId = result.Id,
                    WorkspaceId = result.WorkspaceId,
                    ClientId = result.ClientId,
                    Name = result.Name,
                    Description = result.Description,
                    IsBillable = result.IsBillable,
                    Actor = command.Actor,
                    OccurredAt = result.ModifiedAt
                },
                ct);

            return result.Id;
        }
    }
}