using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using ChronoTrack.Domain.Events.Tasks;
using ChronoTrack.Domain.Tasks;
using MediatR;

namespace ChronoTrack.Application.Tasks.Commands.Remove
{
    public sealed class RemoveProjectTaskCommandHandler
        : IRequestHandler<RemoveProjectTaskCommand, int>
    {
        private readonly IProjectTaskWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public RemoveProjectTaskCommandHandler(
            IProjectTaskWriteRepository repository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            RemoveProjectTaskCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(ProjectTask),
                    command.Id,
                    nameof(RemoveProjectTaskCommandHandler));
            }

            result.Remove(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new ProjectTaskRemoved
                {
                    ProjectTaskId = result.Id,
                    ProjectId = result.ProjectId,
                    Actor = command.Actor,
                    OccurredAt = result.RemovedAt!.Value
                },
                ct);

            return result.Id;
        }
    }
}