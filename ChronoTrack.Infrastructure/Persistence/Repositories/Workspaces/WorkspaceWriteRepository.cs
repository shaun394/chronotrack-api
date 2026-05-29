using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Domain.Workspaces;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.Workspaces
{
    public sealed class WorkspaceWriteRepository : IWorkspaceWriteRepository
    {
        private readonly ApplicationDbContext _db;

        public WorkspaceWriteRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Workspace workspace, CancellationToken ct)
        {
            await _db.Workspaces.AddAsync(workspace, ct);
        }

        public async Task<Workspace?> GetForUpdateAsync(int id, CancellationToken ct)
        {
            return await _db.Workspaces.FindAsync([id], ct);
        }
    }
}