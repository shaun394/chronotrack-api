using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Domain.Tags;
using MediatR;

namespace ChronoTrack.Application.Tags.Commands.Restore
{
    public sealed class RestoreTagCommandHandler
        : IRequestHandler<RestoreTagCommand, int>
    {
        private readonly ITagWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreTagCommandHandler(
            ITagWriteRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RestoreTagCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Tag),
                    command.Id,
                    nameof(RestoreTagCommandHandler));
            }

            result.Restore(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return result.Id;
        }
    }
}