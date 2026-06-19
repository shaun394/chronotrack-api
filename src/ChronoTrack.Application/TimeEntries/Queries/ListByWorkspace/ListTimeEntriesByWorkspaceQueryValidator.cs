using FluentValidation;

namespace ChronoTrack.Application.TimeEntries.Queries.ListByWorkspace
{
    public sealed class ListTimeEntriesByWorkspaceQueryValidator
        : AbstractValidator<ListTimeEntriesByWorkspaceQuery>
    {
        public ListTimeEntriesByWorkspaceQueryValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .GreaterThan(0)
                .WithMessage("Workspace ID is required.");
        }
    }
}