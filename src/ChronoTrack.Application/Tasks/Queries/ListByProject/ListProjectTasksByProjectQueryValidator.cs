using FluentValidation;

namespace ChronoTrack.Application.Tasks.Queries.ListByProject
{
    public sealed class ListProjectTasksByProjectQueryValidator
        : AbstractValidator<ListProjectTasksByProjectQuery>
    {
        public ListProjectTasksByProjectQueryValidator()
        {
            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("Project ID is required.");
        }
    }
}