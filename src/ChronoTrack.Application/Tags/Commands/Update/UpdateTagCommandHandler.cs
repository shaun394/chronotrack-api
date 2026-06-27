using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Domain.Tags;
using MediatR;

namespace ChronoTrack.Application.Tags.Commands.Update
{
    public sealed class UpdateTagCommandHandler
        : IRequestHandler<UpdateTagCommand, int>
    {
        private readonly ITagWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTagCommandHandler(
            ITagWriteRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            UpdateTagCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Tag),
                    command.Id,
                    nameof(UpdateTagCommandHandler));
            }

            result.Update(
                command.Name,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return result.Id;
        }
    }
}