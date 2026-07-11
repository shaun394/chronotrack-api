using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Events.Tags;
using ChronoTrack.Domain.Tags;
using ChronoTrack.Domain.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Tags.Commands.Create
{
    public sealed class CreateTagCommandHandler
        : IRequestHandler<CreateTagCommand, int>
    {
        private readonly ITagWriteRepository _tagWriteRepository;
        private readonly IWorkspaceReadRepository _workspaceReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStore _eventStore;

        public CreateTagCommandHandler(
            ITagWriteRepository tagWriteRepository,
            IWorkspaceReadRepository workspaceReadRepository,
            IUnitOfWork unitOfWork,
            IEventStore eventStore)
        {
            _tagWriteRepository = tagWriteRepository;
            _workspaceReadRepository = workspaceReadRepository;
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }

        public async Task<int> Handle(
            CreateTagCommand command,
            CancellationToken ct)
        {
            var workspace = await _workspaceReadRepository
                .GetByIdAsync(command.WorkspaceId, ct);

            if (workspace is null)
            {
                throw new NotFoundException(
                    nameof(Workspace),
                    command.WorkspaceId,
                    nameof(CreateTagCommandHandler));
            }

            var result = Tag.Create(
                command.WorkspaceId,
                command.Name,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _tagWriteRepository.AddAsync(result, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            await _eventStore.AppendAsync(
                streamId: result.Id.ToString(),
                @event: new TagCreated
                {
                    TagId = result.Id,
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