using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Domain.Clients;
using MediatR;

namespace ChronoTrack.Application.Clients.Commands.Restore
{
    public sealed class RestoreClientCommandHandler
        : IRequestHandler<RestoreClientCommand, int>
    {
        private readonly IClientWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreClientCommandHandler(
            IClientWriteRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RestoreClientCommand command,
            CancellationToken ct)
        {
            var result = await _repository
                .GetForUpdateAsync(command.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Client),
                    command.Id,
                    nameof(RestoreClientCommandHandler));
            }

            result.Restore(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return result.Id;
        }
    }
}