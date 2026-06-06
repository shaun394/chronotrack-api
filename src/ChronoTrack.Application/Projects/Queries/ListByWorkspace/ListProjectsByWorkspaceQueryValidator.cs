using FluentValidation;

namespace ChronoTrack.Application.Projects.Queries.ListByWorkspace
{
    public sealed class ListProjectsByWorkspaceQueryValidator
        : AbstractValidator<ListProjectsByWorkspaceQuery>
    {
        public ListProjectsByWorkspaceQueryValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .GreaterThan(0)
                .WithMessage("Workspace ID is required.");
        }
    }
}