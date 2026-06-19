using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Clients;
using MediatR;

namespace ChronoTrack.Application.Clients.Commands.Create
{
    public sealed class CreateClientCommandHandler
        : IRequestHandler<CreateClientCommand, int>
    {
        private readonly IClientWriteRepository _clientWriteRepository;
        private readonly IWorkspaceReadRepository _workspaceReadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateClientCommandHandler(
            IClientWriteRepository clientWriteRepository,
            IWorkspaceReadRepository workspaceReadRepository,
            IUnitOfWork unitOfWork)
        {
            _clientWriteRepository = clientWriteRepository;
            _workspaceReadRepository = workspaceReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            CreateClientCommand command,
            CancellationToken ct)
        {
            var workspace = await _workspaceReadRepository
                .GetByIdAsync(command.WorkspaceId, ct);

            if (workspace is null)
                throw new NotFoundException("Workspace was not found.");

            var client = Client.Create(
                command.WorkspaceId,
                command.Name,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _clientWriteRepository.AddAsync(client, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return client.Id;
        }
    }
}
