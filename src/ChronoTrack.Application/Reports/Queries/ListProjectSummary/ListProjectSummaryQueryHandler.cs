using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.Reports;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Application.ReadModels.Reports;
using ChronoTrack.Domain.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Reports.Queries.ListProjectSummary
{
    public sealed class ListProjectSummaryQueryHandler
        : IRequestHandler<
            ListProjectSummaryQuery,
            IReadOnlyCollection<ProjectTimeSummaryReadModel>>
    {
        private readonly IReportReadRepository _reportReadRepository;
        private readonly IWorkspaceReadRepository _workspaceReadRepository;

        public ListProjectSummaryQueryHandler(
            IReportReadRepository reportReadRepository,
            IWorkspaceReadRepository workspaceReadRepository)
        {
            _reportReadRepository = reportReadRepository;
            _workspaceReadRepository = workspaceReadRepository;
        }

        public async Task<IReadOnlyCollection<ProjectTimeSummaryReadModel>> Handle(
            ListProjectSummaryQuery query,
            CancellationToken ct)
        {
            var workspace = await _workspaceReadRepository
                .GetByIdAsync(query.WorkspaceId, ct);

            if (workspace is null)
            {
                throw new NotFoundException(
                    nameof(Workspace),
                    query.WorkspaceId,
                    nameof(ListProjectSummaryQueryHandler));
            }

            var result = await _reportReadRepository
                .ListProjectSummaryAsync(
                    query.WorkspaceId,
                    query.From,
                    query.To,
                    ct);

            return result;
        }
    }
}