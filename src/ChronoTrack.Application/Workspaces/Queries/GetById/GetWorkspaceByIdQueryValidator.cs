using FluentValidation;

namespace ChronoTrack.Application.Workspaces.Queries.GetById
{
    public sealed class GetWorkspaceByIdQueryValidator
        : AbstractValidator<GetWorkspaceByIdQuery>
    {
        public GetWorkspaceByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}