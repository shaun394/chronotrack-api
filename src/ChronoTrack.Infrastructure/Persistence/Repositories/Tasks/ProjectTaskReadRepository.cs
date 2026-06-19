using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using ChronoTrack.Application.ReadModels.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.Tasks
{
    public sealed class ProjectTaskReadRepository : IProjectTaskReadRepository
    {
        private readonly ApplicationDbContext _db;

        public ProjectTaskReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ProjectTaskReadModel?> GetByIdAsync(
            int id,
            CancellationToken ct)
        {
            return await _db.ProjectTasks
                .AsNoTracking()
                .Where(x =>
                    x.Id == id
                    && x.RemovedAt == null)
                .Select(x => new ProjectTaskReadModel
                {
                    Id = x.Id,
                    ProjectId = x.ProjectId,
                    Name = x.Name,
                    Description = x.Description,
                    IsBillable = x.IsBillable
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyCollection<ProjectTaskReadModel>> ListByProjectAsync(
            int projectId,
            CancellationToken ct)
        {
            return await _db.ProjectTasks
                .AsNoTracking()
                .Where(x =>
                    x.ProjectId == projectId
                    && x.RemovedAt == null)
                .OrderBy(x => x.Name)
                .Select(x => new ProjectTaskReadModel
                {
                    Id = x.Id,
                    ProjectId = x.ProjectId,
                    Name = x.Name,
                    Description = x.Description,
                    IsBillable = x.IsBillable
                })
                .ToListAsync(ct);
        }
    }
}