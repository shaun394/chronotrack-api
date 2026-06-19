using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using MediatR;

namespace ChronoTrack.Application.Clients.Commands.Update
{
    public sealed class UpdateClientCommandHandler
        : IRequestHandler<UpdateClientCommand, int>
    {
        private readonly IClientWriteRepository _clientWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClientCommandHandler(
            IClientWriteRepository clientWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _clientWriteRepository = clientWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            UpdateClientCommand command,
            CancellationToken ct)
        {
            var client = await _clientWriteRepository
                .GetForUpdateAsync(command.Id, ct);

            if (client is null)
            {
                throw new NotFoundException("Client was not found.");
            }

            client.Update(
                command.Name,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return client.Id;
        }
    }
}