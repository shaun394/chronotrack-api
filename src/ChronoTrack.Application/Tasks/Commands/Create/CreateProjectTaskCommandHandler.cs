using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using ChronoTrack.Domain.Events.Tasks;
using ChronoTrack.Domain.Projects;
using ChronoTrack.Domain.Tasks;
using MediatR;

namespace ChronoTrack.Application.Tasks.Commands.Create
{
    public sealed class CreateProjectTaskCommandHandler
        : IRequestHandler<CreateProjectTaskCommand, int>
    {
        private readonly IProjectTaskWriteRepository _projectTaskWriteRepository;
        private readonly IProjectReadRepository _projectReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public CreateProjectTaskCommandHandler(
            IProjectTaskWriteRepository projectTaskWriteRepository,
            IProjectReadRepository projectReadRepository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _projectTaskWriteRepository = projectTaskWriteRepository;
            _projectReadRepository = projectReadRepository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            CreateProjectTaskCommand command,
            CancellationToken ct)
        {
            var project = await _projectReadRepository
                .GetByIdAsync(command.ProjectId, ct);

            if (project is null)
            {
                throw new NotFoundException(
                    nameof(Project),
                    command.ProjectId,
                    nameof(CreateProjectTaskCommandHandler));
            }

            var result = ProjectTask.Create(
                command.ProjectId,
                command.Name,
                command.Description,
                command.IsBillable,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _projectTaskWriteRepository.AddAsync(result, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new ProjectTaskCreated
                {
                    ProjectTaskId = result.Id,
                    ProjectId = result.ProjectId,
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