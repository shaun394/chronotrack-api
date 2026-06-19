using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using MediatR;

namespace ChronoTrack.Application.Projects.Commands.Remove
{
    public sealed class RemoveProjectCommandHandler
        : IRequestHandler<RemoveProjectCommand, int>
    {
        private readonly IProjectWriteRepository _projectWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProjectCommandHandler(
            IProjectWriteRepository projectWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _projectWriteRepository = projectWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RemoveProjectCommand command,
            CancellationToken ct)
        {
            var project = await _projectWriteRepository
                .GetForUpdateAsync(command.Id, ct);

            if (project is null)
            {
                throw new NotFoundException("Project was not found.");
            }

            project.Remove(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return project.Id;
        }
    }
}