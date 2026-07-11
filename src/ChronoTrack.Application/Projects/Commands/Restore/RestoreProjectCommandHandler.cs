using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Domain.Events.Projects;
using ChronoTrack.Domain.Projects;
using MediatR;

namespace ChronoTrack.Application.Projects.Commands.Restore
{
    public sealed class RestoreProjectCommandHandler
        : IRequestHandler<RestoreProjectCommand, int>
    {
        private readonly IProjectWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public RestoreProjectCommandHandler(
            IProjectWriteRepository repository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            RestoreProjectCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Project),
                    command.Id,
                    nameof(RestoreProjectCommandHandler));
            }

            result.Restore(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new ProjectRestored
                {
                    ProjectId = result.Id,
                    WorkspaceId = result.WorkspaceId,
                    Actor = command.Actor,
                    OccurredAt = result.RestoredAt!.Value
                },
                ct);

            return result.Id;
        }
    }
}