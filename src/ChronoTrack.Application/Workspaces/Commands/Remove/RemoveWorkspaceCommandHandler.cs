using ChronoTrack.Application.Common.EventStore;
using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Events.Workspaces;
using ChronoTrack.Domain.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Remove
{
    public sealed class RemoveWorkspaceCommandHandler
        : IRequestHandler<RemoveWorkspaceCommand, int>
    {
        private readonly IWorkspaceWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public RemoveWorkspaceCommandHandler(
            IWorkspaceWriteRepository repository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            RemoveWorkspaceCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Workspace),
                    command.Id,
                    nameof(RemoveWorkspaceCommandHandler));
            }

            result.Remove(command.Actor, DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new WorkspaceRemoved
                {
                    Id = result.Id,
                    Actor = command.Actor,
                    OccurredAt = DateTimeOffset.UtcNow
                },
                ct);

            return result.Id;
        }
    }
}