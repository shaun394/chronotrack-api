using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Application.Interfaces.Repositories.Tasks;
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

        public CreateProjectTaskCommandHandler(
            IProjectTaskWriteRepository projectTaskWriteRepository,
            IProjectReadRepository projectReadRepository,
            IUnitOfWork unitOfWork)
        {
            _projectTaskWriteRepository = projectTaskWriteRepository;
            _projectReadRepository = projectReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            CreateProjectTaskCommand command,
            CancellationToken ct)
        {
            var project = await _projectReadRepository
                .GetByIdAsync(command.ProjectId, ct);

            if (project is null)
            {
                throw new NotFoundException("Project was not found.");
            }

            var projectTask = ProjectTask.Create(
                command.ProjectId,
                command.Name,
                command.Description,
                command.IsBillable,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _projectTaskWriteRepository.AddAsync(projectTask, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return projectTask.Id;
        }
    }
}