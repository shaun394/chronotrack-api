using ChronoTrack.Application.ReadModels.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Queries.List
{
    public sealed record ListWorkspacesQuery : IRequest<IReadOnlyCollection<WorkspaceReadModel>>;
}