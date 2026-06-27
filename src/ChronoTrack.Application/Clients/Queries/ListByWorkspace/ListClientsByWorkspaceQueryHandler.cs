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
        private readonly IClientReadRepository _repository;

        public ListClientsByWorkspaceQueryHandler(
            IClientReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<ClientReadModel>> Handle(
            ListClientsByWorkspaceQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .ListByWorkspaceAsync(query.WorkspaceId, ct);

            return result;
        }
    }
}