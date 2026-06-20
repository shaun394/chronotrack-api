using ChronoTrack.Application.ReadModels.Reports;
using MediatR;

namespace ChronoTrack.Application.Reports.Queries.ListTagSummary
{
    public sealed record ListTagSummaryQuery(
        int WorkspaceId,
        DateOnly From,
        DateOnly To) : IRequest<IReadOnlyCollection<TagTimeSummaryReadModel>>;
}