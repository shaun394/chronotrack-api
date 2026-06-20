using ChronoTrack.Application.ReadModels.Reports;
using MediatR;

namespace ChronoTrack.Application.Reports.Queries.ListProjectSummary
{
    public sealed record ListProjectSummaryQuery(
        int WorkspaceId,
        DateOnly From,
        DateOnly To) : IRequest<IReadOnlyCollection<ProjectTimeSummaryReadModel>>;
}