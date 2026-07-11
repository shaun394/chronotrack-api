using ChronoTrack.Application.Common.EventStore;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Events.Workspaces;
using ChronoTrack.Domain.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Commands.Create
{
    public sealed class CreateWorkspaceCommandHandler
        : IRequestHandler<CreateWorkspaceCommand, int>
    {
        private readonly IWorkspaceWriteRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public CreateWorkspaceCommandHandler(
            IWorkspaceWriteRepository repository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            CreateWorkspaceCommand command,
            CancellationToken ct)
        {
            var result = Workspace.Create(
                command.Name,
                command.Description,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _repository.AddAsync(result, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new WorkspaceCreated
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    Actor = command.Actor,
                    OccurredAt = DateTimeOffset.Now
                },
                ct);

            return result.Id;
        }
    }
}