using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Application.ReadModels.Tags;
using MediatR;

namespace ChronoTrack.Application.Tags.Queries.ListByWorkspace
{
    public sealed class ListTagsByWorkspaceQueryHandler
        : IRequestHandler<ListTagsByWorkspaceQuery, IReadOnlyCollection<TagReadModel>>
    {
        private readonly ITagReadRepository _tagReadRepository;

        public ListTagsByWorkspaceQueryHandler(ITagReadRepository tagReadRepository)
        {
            _tagReadRepository = tagReadRepository;
        }

        public async Task<IReadOnlyCollection<TagReadModel>> Handle(
            ListTagsByWorkspaceQuery query,
            CancellationToken ct)
        {
            return await _tagReadRepository.ListByWorkspaceAsync(query.WorkspaceId, ct);
        }
    }
}