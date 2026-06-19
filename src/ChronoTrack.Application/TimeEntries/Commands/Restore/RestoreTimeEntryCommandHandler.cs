using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Domain.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntries.Commands.Restore
{
    public sealed class RestoreTimeEntryCommandHandler
        : IRequestHandler<RestoreTimeEntryCommand, int>
    {
        private readonly ITimeEntryWriteRepository _timeEntryWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreTimeEntryCommandHandler(
            ITimeEntryWriteRepository timeEntryWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _timeEntryWriteRepository = timeEntryWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RestoreTimeEntryCommand command,
            CancellationToken ct)
        {
            var timeEntry = await _timeEntryWriteRepository
                .GetForUpdateAsync(command.Id, ct);

            if (timeEntry is null)
            {
                throw new NotFoundException(
                    nameof(TimeEntry),
                    command.Id,
                    nameof(RestoreTimeEntryCommandHandler));
            }

            timeEntry.Restore(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return timeEntry.Id;
        }
    }
}