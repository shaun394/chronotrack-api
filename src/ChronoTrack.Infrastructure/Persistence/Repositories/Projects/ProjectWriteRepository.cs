using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Domain.Projects;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.Projects
{
    public sealed class ProjectWriteRepository : IProjectWriteRepository
    {
        private readonly ApplicationDbContext _db;

        public ProjectWriteRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Project project, CancellationToken ct)
        {
            await _db.Projects.AddAsync(project, ct);
        }

        public async Task<Project?> GetForUpdateAsync(int id, CancellationToken ct)
        {
            return await _db.Projects.FindAsync([id], ct);
        }
    }
}