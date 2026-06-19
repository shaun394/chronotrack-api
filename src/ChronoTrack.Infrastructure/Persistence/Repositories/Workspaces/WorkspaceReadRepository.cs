using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Application.ReadModels.Workspaces;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.Workspaces
{
    public sealed class WorkspaceReadRepository : IWorkspaceReadRepository
    {
        private readonly ApplicationDbContext _db;

        public WorkspaceReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<WorkspaceReadModel?> GetByIdAsync(
            int id,
            CancellationToken ct)
        {
            return await _db.Workspaces
                .AsNoTracking()
                .Where(x =>
                    x.Id == id
                    && x.RemovedAt == null)
                .Select(x => new WorkspaceReadModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyCollection<WorkspaceReadModel>> ListAsync(
            CancellationToken ct)
        {
            return await _db.Workspaces
                .AsNoTracking()
                .Where(x => x.RemovedAt == null)
                .OrderBy(x => x.Name)
                .Select(x => new WorkspaceReadModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .ToListAsync(ct);
        }
    }
}