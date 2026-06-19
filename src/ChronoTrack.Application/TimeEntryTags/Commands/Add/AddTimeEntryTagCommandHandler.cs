using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Application.Interfaces.Repositories.TimeEntryTags;
using ChronoTrack.Domain.Tags;
using ChronoTrack.Domain.TimeEntries;
using ChronoTrack.Domain.TimeEntryTags;
using MediatR;

namespace ChronoTrack.Application.TimeEntryTags.Commands.Add
{
    public sealed class AddTimeEntryTagCommandHandler
        : IRequestHandler<AddTimeEntryTagCommand, int>
    {
        private readonly ITimeEntryTagWriteRepository _timeEntryTagWriteRepository;
        private readonly ITimeEntryReadRepository _timeEntryReadRepository;
        private readonly ITagReadRepository _tagReadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTimeEntryTagCommandHandler(
            ITimeEntryTagWriteRepository timeEntryTagWriteRepository,
            ITimeEntryReadRepository timeEntryReadRepository,
            ITagReadRepository tagReadRepository,
            IUnitOfWork unitOfWork)
        {
            _timeEntryTagWriteRepository = timeEntryTagWriteRepository;
            _timeEntryReadRepository = timeEntryReadRepository;
            _tagReadRepository = tagReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            AddTimeEntryTagCommand command,
            CancellationToken ct)
        {
            var timeEntry = await _timeEntryReadRepository
                .GetByIdAsync(command.TimeEntryId, ct);

            if (timeEntry is null)
            {
                throw new NotFoundException(
                    nameof(TimeEntry),
                    command.TimeEntryId,
                    nameof(AddTimeEntryTagCommandHandler));
            }

            var tag = await _tagReadRepository
                .GetByIdAsync(command.TagId, ct);

            if (tag is null)
            {
                throw new NotFoundException(
                    nameof(Tag),
                    command.TagId,
                    nameof(AddTimeEntryTagCommandHandler));
            }

            if (tag.WorkspaceId != timeEntry.WorkspaceId)
            {
                throw new NotFoundException(
                    nameof(Tag),
                    command.TagId,
                    nameof(AddTimeEntryTagCommandHandler));
            }

            var existingTimeEntryTag = await _timeEntryTagWriteRepository
                .GetForUpdateAsync(
                    command.TimeEntryId,
                    command.TagId,
                    ct);

            if (existingTimeEntryTag is not null && !existingTimeEntryTag.IsRemoved)
            {
                throw new DuplicateEntityException(
                    nameof(TimeEntryTag),
                    nameof(AddTimeEntryTagCommandHandler));
            }

            if (existingTimeEntryTag is not null)
            {
                existingTimeEntryTag.Restore(
                    command.Actor,
                    DateTimeOffset.UtcNow);

                await _unitOfWork.SaveChangesAsync(ct);

                return existingTimeEntryTag.Id;
            }

            var timeEntryTag = TimeEntryTag.Create(
                timeEntry.WorkspaceId,
                command.TimeEntryId,
                command.TagId,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _timeEntryTagWriteRepository.AddAsync(timeEntryTag, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return timeEntryTag.Id;
        }
    }
}