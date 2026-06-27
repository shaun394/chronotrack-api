using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using ChronoTrack.Domain.Tasks;
using MediatR;

namespace ChronoTrack.Application.Tasks.Commands.Remove
{
    public sealed class RemoveProjectTaskCommandHandler
        : IRequestHandler<RemoveProjectTaskCommand, int>
    {
        private readonly IProjectTaskWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProjectTaskCommandHandler(
            IProjectTaskWriteRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
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

            return result.Id;
        }
    }
}