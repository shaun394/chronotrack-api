using ChronoTrack.Application.ReadModels.Tags;
using MediatR;

namespace ChronoTrack.Application.Tags.Queries.ListAudit
{
    public sealed record ListTagAuditEventsQuery(
        int Id) : IRequest<TagAuditReadModel?>;
}