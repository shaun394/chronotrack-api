using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using MediatR;

namespace ChronoTrack.Application.Tasks.Commands.Update
{
    public sealed class UpdateProjectTaskCommandHandler
        : IRequestHandler<UpdateProjectTaskCommand, int>
    {
        private readonly IProjectTaskWriteRepository _projectTaskWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectTaskCommandHandler(
            IProjectTaskWriteRepository projectTaskWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _projectTaskWriteRepository = projectTaskWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            UpdateProjectTaskCommand command,
            CancellationToken ct)
        {
            var projectTask = await _projectTaskWriteRepository.GetForUpdateAsync(command.Id, ct);

            if (projectTask is null)
            {
                throw new NotFoundException("Project task was not found.");
            }

            projectTask.Update(
                command.Name,
                command.Description,
                command.IsBillable,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return projectTask.Id;
        }
    }
}