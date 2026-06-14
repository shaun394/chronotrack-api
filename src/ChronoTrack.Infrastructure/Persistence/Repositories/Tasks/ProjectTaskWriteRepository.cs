using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using ChronoTrack.Domain.Tasks;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.Tasks
{
    public sealed class ProjectTaskWriteRepository : IProjectTaskWriteRepository
    {
        private readonly ApplicationDbContext _db;

        public ProjectTaskWriteRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(ProjectTask projectTask, CancellationToken ct)
        {
            await _db.ProjectTasks.AddAsync(projectTask, ct);
        }

        public async Task<ProjectTask?> GetForUpdateAsync(int id, CancellationToken ct)
        {
            return await _db.ProjectTasks.FindAsync([id], ct);
        }
    }
}