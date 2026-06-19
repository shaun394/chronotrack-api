using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.ReadModels.Clients;
using MediatR;

namespace ChronoTrack.Application.Clients.Queries.ListByWorkspace
{
    public sealed class ListClientsByWorkspaceQueryHandler
        : IRequestHandler<
            ListClientsByWorkspaceQuery,
            IReadOnlyCollection<ClientReadModel>>
    {
        private readonly IClientReadRepository _clientReadRepository;

        public ListClientsByWorkspaceQueryHandler(
            IClientReadRepository clientReadRepository)
        {
            _clientReadRepository = clientReadRepository;
        }

        public async Task<IReadOnlyCollection<ClientReadModel>> Handle(
            ListClientsByWorkspaceQuery query,
            CancellationToken ct)
        {
            return await _clientReadRepository
                .ListByWorkspaceAsync(query.WorkspaceId, ct);
        }
    }
}