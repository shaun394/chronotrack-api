using ChronoTrack.Application.ReadModels.Reports;
using MediatR;

namespace ChronoTrack.Application.Reports.Queries.GetWorkspaceSummary
{
    public sealed record GetWorkspaceSummaryQuery(
        int WorkspaceId,
        DateOnly From,
        DateOnly To) : IRequest<WorkspaceTimeSummaryReadModel>;
}