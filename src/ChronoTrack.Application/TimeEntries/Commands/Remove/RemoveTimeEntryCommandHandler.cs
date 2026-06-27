using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Domain.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntries.Commands.Remove
{
    public sealed class RemoveTimeEntryCommandHandler
        : IRequestHandler<RemoveTimeEntryCommand, int>
    {
        private readonly ITimeEntryWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTimeEntryCommandHandler(
            ITimeEntryWriteRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RemoveTimeEntryCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(TimeEntry),
                    command.Id,
                    nameof(RemoveTimeEntryCommandHandler));
            }

            result.Remove(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return result.Id;
        }
    }
}