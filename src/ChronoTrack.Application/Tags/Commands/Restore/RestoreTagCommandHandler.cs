using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using MediatR;

namespace ChronoTrack.Application.Tags.Commands.Restore
{
    public sealed class RestoreTagCommandHandler
        : IRequestHandler<RestoreTagCommand, int>
    {
        private readonly ITagWriteRepository _tagWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreTagCommandHandler(
            ITagWriteRepository tagWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _tagWriteRepository = tagWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RestoreTagCommand command,
            CancellationToken ct)
        {
            var tag = await _tagWriteRepository
                .GetForUpdateAsync(command.Id, ct);

            if (tag is null)
            {
                throw new NotFoundException("Tag was not found.");
            }

            tag.Restore(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return tag.Id;
        }
    }
}