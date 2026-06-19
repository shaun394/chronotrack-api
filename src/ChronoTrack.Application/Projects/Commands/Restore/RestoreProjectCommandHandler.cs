using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Domain.Projects;
using MediatR;

namespace ChronoTrack.Application.Projects.Commands.Restore
{
    public sealed class RestoreProjectCommandHandler
        : IRequestHandler<RestoreProjectCommand, int>
    {
        private readonly IProjectWriteRepository _projectWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreProjectCommandHandler(
            IProjectWriteRepository projectWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _projectWriteRepository = projectWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RestoreProjectCommand command,
            CancellationToken ct)
        {
            var project = await _projectWriteRepository
                .GetForUpdateAsync(command.Id, ct);

            if (project is null)
            {
                throw new NotFoundException(
                    nameof(Project),
                    command.Id,
                    nameof(RestoreProjectCommandHandler));
            }

            project.Restore(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return project.Id;
        }
    }
}