using FluentValidation;

namespace ChronoTrack.Application.Reports.Queries.GetWorkspaceSummary
{
    public sealed class GetWorkspaceSummaryQueryValidator
        : AbstractValidator<GetWorkspaceSummaryQuery>
    {
        public GetWorkspaceSummaryQueryValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .GreaterThan(0)
                .WithMessage("Workspace ID is required.");

            RuleFor(x => x.To)
                .GreaterThanOrEqualTo(x => x.From)
                .WithMessage("To date must be greater than or equal to from date.");
        }
    }
}