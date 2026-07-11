using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Domain.Clients;
using ChronoTrack.Domain.Events.Clients;
using MediatR;

namespace ChronoTrack.Application.Clients.Commands.Remove
{
    public sealed class RemoveClientCommandHandler
        : IRequestHandler<RemoveClientCommand, int>
    {
        private readonly IClientWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public RemoveClientCommandHandler(
            IClientWriteRepository repository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            RemoveClientCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Client),
                    command.Id,
                    nameof(RemoveClientCommandHandler));
            }

            result.Remove(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new ClientRemoved
                {
                    ClientId = result.Id,
                    WorkspaceId = result.WorkspaceId,
                    Actor = command.Actor,
                    OccurredAt = result.RemovedAt!.Value
                },
                ct);

            return result.Id;
        }
    }
}