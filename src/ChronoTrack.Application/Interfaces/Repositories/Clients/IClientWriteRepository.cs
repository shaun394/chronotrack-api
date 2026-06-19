using ChronoTrack.Domain.Clients;

namespace ChronoTrack.Application.Interfaces.Repositories.Clients
{
    public interface IClientWriteRepository
    {
        Task AddAsync(
            Client client,
            CancellationToken ct);

        Task<Client?> GetForUpdateAsync(
            int id,
            CancellationToken ct);
    }
}
