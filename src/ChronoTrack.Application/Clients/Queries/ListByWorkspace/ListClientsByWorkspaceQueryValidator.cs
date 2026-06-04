using FluentValidation;

namespace ChronoTrack.Application.Clients.Queries.ListByWorkspace
{
    public sealed class ListClientsByWorkspaceQueryValidator
        : AbstractValidator<ListClientsByWorkspaceQuery>
    {
        public ListClientsByWorkspaceQueryValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .GreaterThan(0)
                .WithMessage("Workspace ID is required.");
        }
    }
}