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
        private readonly IClientWriteRepository _clientWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreClientCommandHandler(
            IClientWriteRepository clientWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _clientWriteRepository = clientWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            RestoreClientCommand command,
            CancellationToken ct)
        {
            var client = await _clientWriteRepository
                .GetForUpdateAsync(command.Id, ct);

            if (client is null)
            {
                throw new NotFoundException(
                    nameof(Client),
                    command.Id,
                    nameof(RestoreClientCommandHandler));
            }

            client.Restore(
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return client.Id;
        }
    }
}