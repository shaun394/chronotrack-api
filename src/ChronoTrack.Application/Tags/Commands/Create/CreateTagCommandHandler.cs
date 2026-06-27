using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
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

        public CreateTagCommandHandler(
            ITagWriteRepository tagWriteRepository,
            IWorkspaceReadRepository workspaceReadRepository,
            IUnitOfWork unitOfWork)
        {
            _tagWriteRepository = tagWriteRepository;
            _workspaceReadRepository = workspaceReadRepository;
            _unitOfWork = unitOfWork;
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

            return result.Id;
        }
    }
}