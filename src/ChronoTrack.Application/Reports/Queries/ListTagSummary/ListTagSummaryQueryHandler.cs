using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.Reports;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Application.ReadModels.Reports;
using ChronoTrack.Domain.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Reports.Queries.ListTagSummary
{
    public sealed class ListTagSummaryQueryHandler
        : IRequestHandler<
            ListTagSummaryQuery,
            IReadOnlyCollection<TagTimeSummaryReadModel>>
    {
        private readonly IReportReadRepository _reportReadRepository;
        private readonly IWorkspaceReadRepository _workspaceReadRepository;

        public ListTagSummaryQueryHandler(
            IReportReadRepository reportReadRepository,
            IWorkspaceReadRepository workspaceReadRepository)
        {
            _reportReadRepository = reportReadRepository;
            _workspaceReadRepository = workspaceReadRepository;
        }

        public async Task<IReadOnlyCollection<TagTimeSummaryReadModel>> Handle(
            ListTagSummaryQuery query,
            CancellationToken ct)
        {
            var workspace = await _workspaceReadRepository
                .GetByIdAsync(query.WorkspaceId, ct);

            if (workspace is null)
            {
                throw new NotFoundException(
                    nameof(Workspace),
                    query.WorkspaceId,
                    nameof(ListTagSummaryQueryHandler));
            }

            var result = await _reportReadRepository
                .ListTagSummaryAsync(
                    query.WorkspaceId,
                    query.From,
                    query.To,
                    ct);

            return result;
        }
    }
}