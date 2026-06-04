using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Domain.Clients;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.Clients
{
    public sealed class ClientWriteRepository : IClientWriteRepository
    {
        private readonly ApplicationDbContext _db;

        public ClientWriteRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Client client, CancellationToken ct)
        {
            await _db.Clients.AddAsync(client, ct);
        }

        public async Task<Client?> GetForUpdateAsync(int id, CancellationToken ct)
        {
            return await _db.Clients.FindAsync([id], ct);
        }
    }
}