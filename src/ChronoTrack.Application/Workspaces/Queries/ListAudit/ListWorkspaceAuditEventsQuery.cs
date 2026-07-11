using ChronoTrack.Application.ReadModels.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Queries.ListAudit
{
    public sealed record ListWorkspaceAuditEventsQuery(
        int Id) : IRequest<WorkspaceAuditReadModel?>;
}