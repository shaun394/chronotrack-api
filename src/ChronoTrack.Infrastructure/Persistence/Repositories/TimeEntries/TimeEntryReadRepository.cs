using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Application.ReadModels.TimeEntries;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.TimeEntries
{
    public sealed class TimeEntryReadRepository : ITimeEntryReadRepository
    {
        private readonly ApplicationDbContext _db;

        public TimeEntryReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<TimeEntryReadModel?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _db.TimeEntries
                .AsNoTracking()
                .Where(x => x.Id == id && x.RemovedAt == null)
                .Select(x => new TimeEntryReadModel
                {
                    Id = x.Id,
                    WorkspaceId = x.WorkspaceId,
                    ProjectId = x.ProjectId,
                    ProjectTaskId = x.ProjectTaskId,
                    ClientId = x.ClientId,
                    Description = x.Description,
                    WorkDate = x.WorkDate,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    DurationMinutes = x.DurationMinutes,
                    IsBillable = x.IsBillable,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyCollection<TimeEntryReadModel>> ListByWorkspaceAsync(
            int workspaceId,
            CancellationToken ct)
        {
            return await _db.TimeEntries
                .AsNoTracking()
                .Where(x => x.WorkspaceId == workspaceId && x.RemovedAt == null)
                .OrderByDescending(x => x.WorkDate)
                .ThenByDescending(x => x.StartTime)
                .Select(x => new TimeEntryReadModel
                {
                    Id = x.Id,
                    WorkspaceId = x.WorkspaceId,
                    ProjectId = x.ProjectId,
                    ProjectTaskId = x.ProjectTaskId,
                    ClientId = x.ClientId,
                    Description = x.Description,
                    WorkDate = x.WorkDate,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    DurationMinutes = x.DurationMinutes,
                    IsBillable = x.IsBillable,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                })
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyCollection<TimeEntryReadModel>> ListByProjectAsync(
            int projectId,
            CancellationToken ct)
        {
            return await _db.TimeEntries
                .AsNoTracking()
                .Where(x => x.ProjectId == projectId && x.RemovedAt == null)
                .OrderByDescending(x => x.WorkDate)
                .ThenByDescending(x => x.StartTime)
                .Select(x => new TimeEntryReadModel
                {
                    Id = x.Id,
                    WorkspaceId = x.WorkspaceId,
                    ProjectId = x.ProjectId,
                    ProjectTaskId = x.ProjectTaskId,
                    ClientId = x.ClientId,
                    Description = x.Description,
                    WorkDate = x.WorkDate,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    DurationMinutes = x.DurationMinutes,
                    IsBillable = x.IsBillable,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                })
                .ToListAsync(ct);
        }
    }
}