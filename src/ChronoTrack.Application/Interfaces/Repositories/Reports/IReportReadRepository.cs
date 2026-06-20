using ChronoTrack.Application.ReadModels.Reports;

namespace ChronoTrack.Application.Interfaces.Repositories.Reports
{
    public interface IReportReadRepository
    {
        Task<WorkspaceTimeSummaryReadModel> GetWorkspaceSummaryAsync(
            int workspaceId,
            DateOnly from,
            DateOnly to,
            CancellationToken ct);

        Task<IReadOnlyCollection<DailyTimeSummaryReadModel>> ListDailySummaryAsync(
            int workspaceId,
            DateOnly from,
            DateOnly to,
            CancellationToken ct);

        Task<IReadOnlyCollection<ProjectTimeSummaryReadModel>> ListProjectSummaryAsync(
            int workspaceId,
            DateOnly from,
            DateOnly to,
            CancellationToken ct);

        Task<IReadOnlyCollection<TagTimeSummaryReadModel>> ListTagSummaryAsync(
            int workspaceId,
            DateOnly from,
            DateOnly to,
            CancellationToken ct);
    }
}