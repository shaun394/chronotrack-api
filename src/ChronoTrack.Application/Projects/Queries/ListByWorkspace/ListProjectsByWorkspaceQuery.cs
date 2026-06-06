using ChronoTrack.Application.ReadModels.Projects;
using MediatR;

namespace ChronoTrack.Application.Projects.Queries.ListByWorkspace
{
    public sealed record ListProjectsByWorkspaceQuery(int WorkspaceId)
        : IRequest<IReadOnlyCollection<ProjectReadModel>>;
}