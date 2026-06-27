using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.TimeEntryTags;
using ChronoTrack.Domain.TimeEntryTags;
using MediatR;

namespace ChronoTrack.Application.TimeEntryTags.Commands.Remove
{
    public sealed class RemoveTimeEntryTagCommandHandler
        : IRequestHandler<RemoveTimeEntryTagCommand, int>
    {
        private readonly ITimeEntryTagWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTimeEntryTagCommandHandler(
            ITimeEntryTagWriteRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RemoveTimeEntryTagCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(
                    command.TimeEntryId,
                    command.TagId,
                    ct);

            if (result is null || result.IsRemoved)
            {
                throw new NotFoundException(
                    nameof(TimeEntryTag),
                    $"{command.TimeEntryId}:{command.TagId}",
                    nameof(RemoveTimeEntryTagCommandHandler));
            }

            result.Remove(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return result.Id;
        }
    }
}