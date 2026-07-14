using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using ChronoTrack.Domain.Events.Tasks;
using ChronoTrack.Domain.Tasks;
using MediatR;

namespace ChronoTrack.Application.Tasks.Commands.Update
{
    public sealed class UpdateProjectTaskCommandHandler
        : IRequestHandler<UpdateProjectTaskCommand, int>
    {
        private readonly IProjectTaskWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public UpdateProjectTaskCommandHandler(
            IProjectTaskWriteRepository repository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            UpdateProjectTaskCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(ProjectTask),
                    command.Id,
                    nameof(UpdateProjectTaskCommandHandler));
            }

            result.Update(
                command.Name,
                command.Description,
                command.IsBillable,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new ProjectTaskUpdated
                {
                    ProjectTaskId = result.Id,
                    ProjectId = result.ProjectId,
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