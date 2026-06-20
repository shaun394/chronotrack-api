using ChronoTrack.Application.Interfaces.Repositories.Reports;
using ChronoTrack.Application.ReadModels.Reports;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrack.Infrastructure.Persistence.Repositories.Reports
{
    public sealed class ReportReadRepository : IReportReadRepository
    {
        private readonly ApplicationDbContext _db;

        public ReportReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<WorkspaceTimeSummaryReadModel> GetWorkspaceSummaryAsync(
            int workspaceId,
            DateOnly from,
            DateOnly to,
            CancellationToken ct)
        {
            var result = await _db.TimeEntries
                .AsNoTracking()
                .Where(x =>
                    x.WorkspaceId == workspaceId &&
                    x.WorkDate >= from &&
                    x.WorkDate <= to &&
                    x.RemovedAt == null)
                .GroupBy(x => x.WorkspaceId)
                .Select(x => new WorkspaceTimeSummaryReadModel
                {
                    WorkspaceId = workspaceId,
                    From = from,
                    To = to,
                    TotalMinutes = x.Sum(y => y.DurationMinutes),
                    BillableMinutes = x.Sum(y => y.IsBillable ? y.DurationMinutes : 0),
                    NonBillableMinutes = x.Sum(y => !y.IsBillable ? y.DurationMinutes : 0),
                    EntryCount = x.Count()
                })
                .FirstOrDefaultAsync(ct);

            return result ?? new WorkspaceTimeSummaryReadModel
            {
                WorkspaceId = workspaceId,
                From = from,
                To = to,
                TotalMinutes = 0,
                BillableMinutes = 0,
                NonBillableMinutes = 0,
                EntryCount = 0
            };
        }

        public async Task<IReadOnlyCollection<DailyTimeSummaryReadModel>> ListDailySummaryAsync(
            int workspaceId,
            DateOnly from,
            DateOnly to,
            CancellationToken ct)
        {
            return await _db.TimeEntries
                .AsNoTracking()
                .Where(x =>
                    x.WorkspaceId == workspaceId &&
                    x.WorkDate >= from &&
                    x.WorkDate <= to &&
                    x.RemovedAt == null)
                .GroupBy(x => x.WorkDate)
                .Select(x => new DailyTimeSummaryReadModel
                {
                    WorkDate = x.Key,
                    TotalMinutes = x.Sum(y => y.DurationMinutes),
                    BillableMinutes = x.Sum(y => y.IsBillable ? y.DurationMinutes : 0),
                    NonBillableMinutes = x.Sum(y => !y.IsBillable ? y.DurationMinutes : 0),
                    EntryCount = x.Count()
                })
                .OrderBy(x => x.WorkDate)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyCollection<ProjectTimeSummaryReadModel>> ListProjectSummaryAsync(
            int workspaceId,
            DateOnly from,
            DateOnly to,
            CancellationToken ct)
        {
            return await _db.TimeEntries
                .AsNoTracking()
                .Where(x =>
                    x.WorkspaceId == workspaceId &&
                    x.WorkDate >= from &&
                    x.WorkDate <= to &&
                    x.RemovedAt == null)
                .Join(
                    _db.Projects
                        .AsNoTracking()
                        .Where(x => x.RemovedAt == null),
                    timeEntry => timeEntry.ProjectId,
                    project => project.Id,
                    (timeEntry, project) => new
                    {
                        timeEntry.ProjectId,
                        ProjectName = project.Name,
                        timeEntry.DurationMinutes,
                        timeEntry.IsBillable
                    })
                .GroupBy(x => new
                {
                    x.ProjectId,
                    x.ProjectName
                })
                .Select(x => new ProjectTimeSummaryReadModel
                {
                    ProjectId = x.Key.ProjectId,
                    ProjectName = x.Key.ProjectName,
                    TotalMinutes = x.Sum(y => y.DurationMinutes),
                    BillableMinutes = x.Sum(y => y.IsBillable ? y.DurationMinutes : 0),
                    NonBillableMinutes = x.Sum(y => !y.IsBillable ? y.DurationMinutes : 0),
                    EntryCount = x.Count()
                })
                .OrderBy(x => x.ProjectName)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyCollection<TagTimeSummaryReadModel>> ListTagSummaryAsync(
            int workspaceId,
            DateOnly from,
            DateOnly to,
            CancellationToken ct)
        {
            return await _db.TimeEntryTags
                .AsNoTracking()
                .Where(x =>
                    x.WorkspaceId == workspaceId &&
                    x.RemovedAt == null)
                .Join(
                    _db.TimeEntries
                        .AsNoTracking()
                        .Where(x =>
                            x.WorkspaceId == workspaceId &&
                            x.WorkDate >= from &&
                            x.WorkDate <= to &&
                            x.RemovedAt == null),
                    timeEntryTag => timeEntryTag.TimeEntryId,
                    timeEntry => timeEntry.Id,
                    (timeEntryTag, timeEntry) => new
                    {
                        timeEntryTag.TagId,
                        timeEntry.DurationMinutes
                    })
                .Join(
                    _db.Tags
                        .AsNoTracking()
                        .Where(x => x.RemovedAt == null),
                    timeEntryTag => timeEntryTag.TagId,
                    tag => tag.Id,
                    (timeEntryTag, tag) => new
                    {
                        TagId = tag.Id,
                        TagName = tag.Name,
                        timeEntryTag.DurationMinutes
                    })
                .GroupBy(x => new
                {
                    x.TagId,
                    x.TagName
                })
                .Select(x => new TagTimeSummaryReadModel
                {
                    TagId = x.Key.TagId,
                    TagName = x.Key.TagName,
                    TotalMinutes = x.Sum(y => y.DurationMinutes),
                    EntryCount = x.Count()
                })
                .OrderBy(x => x.TagName)
                .ToListAsync(ct);
        }
    }
}