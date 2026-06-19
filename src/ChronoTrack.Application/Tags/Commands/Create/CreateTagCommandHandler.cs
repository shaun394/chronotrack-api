using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Tags;
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
                throw new NotFoundException("Workspace was not found.");
            }

            var tag = Tag.Create(
                command.WorkspaceId,
                command.Name,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _tagWriteRepository.AddAsync(tag, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return tag.Id;
        }
    }
}