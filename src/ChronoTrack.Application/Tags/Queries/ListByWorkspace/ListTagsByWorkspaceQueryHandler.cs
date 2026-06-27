using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Application.ReadModels.Tags;
using MediatR;

namespace ChronoTrack.Application.Tags.Queries.ListByWorkspace
{
    public sealed class ListTagsByWorkspaceQueryHandler
        : IRequestHandler<
            ListTagsByWorkspaceQuery,
            IReadOnlyCollection<TagReadModel>>
    {
        private readonly ITagReadRepository _repository;

        public ListTagsByWorkspaceQueryHandler(
            ITagReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<TagReadModel>> Handle(
            ListTagsByWorkspaceQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .ListByWorkspaceAsync(query.WorkspaceId, ct);

            return result;
        }
    }
}