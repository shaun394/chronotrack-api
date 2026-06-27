using ChronoTrack.Application.ReadModels.Audit.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Workspaces.Queries.ListAudit
{
    public sealed record ListWorkspaceAuditEventsQuery(
        int WorkspaceId)
        : IRequest<IReadOnlyCollection<WorkspaceAuditReadModel>>;
}