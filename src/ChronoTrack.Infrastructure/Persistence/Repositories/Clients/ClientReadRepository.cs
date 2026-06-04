using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.ReadModels.Clients;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.Clients
{
    public sealed class ClientReadRepository : IClientReadRepository
    {
        private readonly ApplicationDbContext _db;

        public ClientReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ClientReadModel?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _db.Clients
                .AsNoTracking()
                .Where(x => x.Id == id && x.RemovedAt == null)
                .Select(x => new ClientReadModel
                {
                    Id = x.Id,
                    WorkspaceId = x.WorkspaceId,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyCollection<ClientReadModel>> ListByWorkspaceAsync(
            int workspaceId,
            CancellationToken ct)
        {
            return await _db.Clients
                .AsNoTracking()
                .Where(x => x.WorkspaceId == workspaceId && x.RemovedAt == null)
                .OrderBy(x => x.Name)
                .Select(x => new ClientReadModel
                {
                    Id = x.Id,
                    WorkspaceId = x.WorkspaceId,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                })
                .ToListAsync(ct);
        }
    }
}