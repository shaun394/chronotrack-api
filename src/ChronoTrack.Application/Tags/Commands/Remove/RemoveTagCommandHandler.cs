using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Domain.Events.Tags;
using ChronoTrack.Domain.Tags;
using MediatR;

namespace ChronoTrack.Application.Tags.Commands.Remove
{
    public sealed class RemoveTagCommandHandler
        : IRequestHandler<RemoveTagCommand, int>
    {
        private readonly ITagWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public RemoveTagCommandHandler(
            ITagWriteRepository repository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            RemoveTagCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Tag),
                    command.Id,
                    nameof(RemoveTagCommandHandler));
            }

            result.Remove(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new TagRemoved
                {
                    TagId = result.Id,
                    WorkspaceId = result.WorkspaceId,
                    Actor = command.Actor,
                    OccurredAt = result.RemovedAt!.Value
                },
                ct);

            return result.Id;
        }
    }
}