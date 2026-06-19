using FluentValidation;

namespace ChronoTrack.Application.TimeEntries.Queries.ListByProject
{
    public sealed class ListTimeEntriesByProjectQueryValidator
        : AbstractValidator<ListTimeEntriesByProjectQuery>
    {
        public ListTimeEntriesByProjectQueryValidator()
        {
            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("Project ID is required.");
        }
    }
}