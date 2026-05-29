using ChronoTrack.Application.ReadModels.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Queries.GetById
{
    public sealed record GetWorkspaceByIdQuery(int Id) : IRequest<WorkspaceReadModel>;
}