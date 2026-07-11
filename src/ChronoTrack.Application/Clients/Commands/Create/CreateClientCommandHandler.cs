using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Clients;
using ChronoTrack.Domain.Events.Clients;
using ChronoTrack.Domain.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Clients.Commands.Create
{
    public sealed class CreateClientCommandHandler
        : IRequestHandler<CreateClientCommand, int>
    {
        private readonly IClientWriteRepository _clientWriteRepository;
        private readonly IWorkspaceReadRepository _workspaceReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public CreateClientCommandHandler(
            IClientWriteRepository clientWriteRepository,
            IWorkspaceReadRepository workspaceReadRepository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _clientWriteRepository = clientWriteRepository;
            _workspaceReadRepository = workspaceReadRepository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            CreateClientCommand command,
            CancellationToken ct)
        {
            var workspace = await _workspaceReadRepository
                .GetByIdAsync(command.WorkspaceId, ct);

            if (workspace is null)
            {
                throw new NotFoundException(
                    nameof(Workspace),
                    command.WorkspaceId,
                    nameof(CreateClientCommandHandler));
            }

            var result = Client.Create(
                command.WorkspaceId,
                command.Name,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _clientWriteRepository.AddAsync(result, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new ClientCreated
                {
                    ClientId = result.Id,
                    WorkspaceId = result.WorkspaceId,
                    Name = result.Name,
                    Actor = command.Actor,
                    OccurredAt = result.CreatedAt
                },
                ct);

            return result.Id;
        }
    }
}