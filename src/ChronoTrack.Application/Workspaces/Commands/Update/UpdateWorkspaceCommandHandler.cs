using ChronoTrack.Application.Common.EventStore;
using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Events.Workspaces;
using ChronoTrack.Domain.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Update
{
    public sealed class UpdateWorkspaceCommandHandler
        : IRequestHandler<UpdateWorkspaceCommand, int>
    {
        private readonly IWorkspaceWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public UpdateWorkspaceCommandHandler(
            IWorkspaceWriteRepository repository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            UpdateWorkspaceCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Workspace),
                    command.Id,
                    nameof(UpdateWorkspaceCommandHandler));
            }

            result.Update(
                command.Name,
                command.Description,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                EventStreamNames.Workspace(result.Id),
                new WorkspaceUpdated
                {
                    WorkspaceId = result.Id,
                    Name = result.Name,
                    Actor = command.Actor,
                    OccurredAt = DateTimeOffset.Now
                },
                ct);

            return result.Id;
        }
    }
}