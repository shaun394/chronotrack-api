using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using ChronoTrack.Domain.Events.Tasks;
using ChronoTrack.Domain.Tasks;
using MediatR;

namespace ChronoTrack.Application.Tasks.Commands.Restore
{
    public sealed class RestoreProjectTaskCommandHandler
        : IRequestHandler<RestoreProjectTaskCommand, int>
    {
        private readonly IProjectTaskWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public RestoreProjectTaskCommandHandler(
            IProjectTaskWriteRepository repository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            RestoreProjectTaskCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(ProjectTask),
                    command.Id,
                    nameof(RestoreProjectTaskCommandHandler));
            }

            result.Restore(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new ProjectTaskRestored
                {
                    ProjectTaskId = result.Id,
                    ProjectId = result.ProjectId,
                    Actor = command.Actor,
                    OccurredAt = result.RestoredAt!.Value
                },
                ct);

            return result.Id;
        }
    }
}