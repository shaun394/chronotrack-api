using FluentValidation;

namespace ChronoTrack.Application.Tags.Queries.ListByWorkspace
{
    public sealed class ListTagsByWorkspaceQueryValidator
        : AbstractValidator<ListTagsByWorkspaceQuery>
    {
        public ListTagsByWorkspaceQueryValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .GreaterThan(0)
                .WithMessage("Workspace ID is required.");
        }
    }
}