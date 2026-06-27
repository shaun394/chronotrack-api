using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using ChronoTrack.Domain.Tasks;
using MediatR;

namespace ChronoTrack.Application.Tasks.Commands.Update
{
    public sealed class UpdateProjectTaskCommandHandler
        : IRequestHandler<UpdateProjectTaskCommand, int>
    {
        private readonly IProjectTaskWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectTaskCommandHandler(
            IProjectTaskWriteRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
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

            return result.Id;
        }
    }
}