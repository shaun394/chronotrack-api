using ChronoTrack.Application.ReadModels.Clients;
using MediatR;

namespace ChronoTrack.Application.Clients.Queries.ListAudit
{
    public sealed record ListClientAuditEventsQuery(
        int Id) : IRequest<ClientAuditReadModel?>;
}