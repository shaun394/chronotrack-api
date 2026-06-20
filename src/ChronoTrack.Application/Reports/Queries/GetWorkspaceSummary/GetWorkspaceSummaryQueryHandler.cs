using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.Reports;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Application.ReadModels.Reports;
using ChronoTrack.Domain.Workspaces;
using MediatR;

namespace ChronoTrack.Application.Reports.Queries.GetWorkspaceSummary
{
    public sealed class GetWorkspaceSummaryQueryHandler
        : IRequestHandler<
            GetWorkspaceSummaryQuery,
            WorkspaceTimeSummaryReadModel>
    {
        private readonly IReportReadRepository _reportReadRepository;
        private readonly IWorkspaceReadRepository _workspaceReadRepository;

        public GetWorkspaceSummaryQueryHandler(
            IReportReadRepository reportReadRepository,
            IWorkspaceReadRepository workspaceReadRepository)
        {
            _reportReadRepository = reportReadRepository;
            _workspaceReadRepository = workspaceReadRepository;
        }

        public async Task<WorkspaceTimeSummaryReadModel> Handle(
            GetWorkspaceSummaryQuery query,
            CancellationToken ct)
        {
            var workspace = await _workspaceReadRepository
                .GetByIdAsync(query.WorkspaceId, ct);

            if (workspace is null)
            {
                throw new NotFoundException(
                    nameof(Workspace),
                    query.WorkspaceId,
                    nameof(GetWorkspaceSummaryQueryHandler));
            }

            var result = await _reportReadRepository
                .GetWorkspaceSummaryAsync(
                query.WorkspaceId,
                query.From,
                query.To,
                ct);

            return result;
        }
    }
}