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
        private readonly ITimeEntryTagWriteRepository _timeEntryTagWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTimeEntryTagCommandHandler(
            ITimeEntryTagWriteRepository timeEntryTagWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _timeEntryTagWriteRepository = timeEntryTagWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RemoveTimeEntryTagCommand command,
            CancellationToken ct)
        {
            var timeEntryTag = await _timeEntryTagWriteRepository
                .GetForUpdateAsync(
                    command.TimeEntryId,
                    command.TagId,
                    ct);

            if (timeEntryTag is null || timeEntryTag.IsRemoved)
            {
                throw new NotFoundException(
                    nameof(TimeEntryTag),
                    $"{command.TimeEntryId}:{command.TagId}",
                    nameof(RemoveTimeEntryTagCommandHandler));
            }

            timeEntryTag.Remove(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return timeEntryTag.Id;
        }
    }
}