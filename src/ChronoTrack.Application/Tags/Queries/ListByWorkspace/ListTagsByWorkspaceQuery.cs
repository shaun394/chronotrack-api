using ChronoTrack.Application.ReadModels.Tags;
using MediatR;

namespace ChronoTrack.Application.Tags.Queries.ListByWorkspace
{
    public sealed record ListTagsByWorkspaceQuery(
        int WorkspaceId) : IRequest<IReadOnlyCollection<TagReadModel>>;
}