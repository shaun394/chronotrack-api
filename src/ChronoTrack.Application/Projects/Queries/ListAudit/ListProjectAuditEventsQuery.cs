using ChronoTrack.Application.ReadModels.Projects;
using MediatR;

namespace ChronoTrack.Application.Projects.Queries.ListAudit
{
    public sealed record ListProjectAuditEventsQuery(
        int Id) : IRequest<ProjectAuditReadModel?>;
}