using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Application.ReadModels.Projects;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.Projects
{
    public sealed class ProjectReadRepository : IProjectReadRepository
    {
        private readonly ApplicationDbContext _db;

        public ProjectReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ProjectReadModel?> GetByIdAsync(
            int id,
            CancellationToken ct)
        {
            return await _db.Projects
                .AsNoTracking()
                .Where(x =>
                    x.Id == id
                    && x.RemovedAt == null)
                .Select(x => new ProjectReadModel
                {
                    Id = x.Id,
                    WorkspaceId = x.WorkspaceId,
                    ClientId = x.ClientId,
                    Name = x.Name,
                    Description = x.Description,
                    IsBillable = x.IsBillable
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyCollection<ProjectReadModel>> ListByWorkspaceAsync(
            int workspaceId,
            CancellationToken ct)
        {
            return await _db.Projects
                .AsNoTracking()
                .Where(x =>
                    x.WorkspaceId == workspaceId
                    && x.RemovedAt == null)
                .OrderBy(x => x.Name)
                .Select(x => new ProjectReadModel
                {
                    Id = x.Id,
                    WorkspaceId = x.WorkspaceId,
                    ClientId = x.ClientId,
                    Name = x.Name,
                    Description = x.Description,
                    IsBillable = x.IsBillable
                })
                .ToListAsync(ct);
        }
    }
}