using ChronoTrack.Application.ReadModels.Reports;
using MediatR;

namespace ChronoTrack.Application.Reports.Queries.ListDailySummary
{
    public sealed record ListDailySummaryQuery(
        int WorkspaceId,
        DateOnly From,
        DateOnly To) : IRequest<IReadOnlyCollection<DailyTimeSummaryReadModel>>;
}