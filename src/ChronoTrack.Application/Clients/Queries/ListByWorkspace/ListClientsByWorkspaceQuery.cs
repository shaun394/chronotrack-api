using ChronoTrack.Application.ReadModels.Clients;
using MediatR;

namespace ChronoTrack.Application.Clients.Queries.ListByWorkspace
{
    public sealed record ListClientsByWorkspaceQuery(int WorkspaceId)
        : IRequest<IReadOnlyCollection<ClientReadModel>>;
}