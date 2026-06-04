using ChronoTrack.Application.ReadModels.Clients;

namespace ChronoTrack.Application.Interfaces.Repositories.Clients
{
    public interface IClientReadRepository
    {
        Task<ClientReadModel?> GetByIdAsync(int id, CancellationToken ct);

        Task<IReadOnlyCollection<ClientReadModel>> ListByWorkspaceAsync(int workspaceId, CancellationToken ct);
    }
}
