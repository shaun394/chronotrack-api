using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Domain.Tags;
using MediatR;

namespace ChronoTrack.Application.Tags.Commands.Remove
{
    public sealed class RemoveTagCommandHandler
        : IRequestHandler<RemoveTagCommand, int>
    {
        private readonly ITagWriteRepository _tagWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTagCommandHandler(
            ITagWriteRepository tagWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _tagWriteRepository = tagWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RemoveTagCommand command,
            CancellationToken ct)
        {
            var tag = await _tagWriteRepository
                .GetForUpdateAsync(command.Id, ct);

            if (tag is null)
            {
                throw new NotFoundException(
                    nameof(Tag),
                    command.Id,
                    nameof(RemoveTagCommandHandler));
            }

            tag.Remove(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return tag.Id;
        }
    }
}