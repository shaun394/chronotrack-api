using ChronoTrack.Application.ReadModels.Tasks;
using MediatR;

namespace ChronoTrack.Application.Tasks.Queries.ListAudit
{
    public sealed record ListProjectTaskAuditEventsQuery(
        int Id) : IRequest<ProjectTaskAuditReadModel?>;
}