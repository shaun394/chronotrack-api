using FluentValidation;

namespace ChronoTrack.Application.TimeEntryTags.Queries.ListByTimeEntry
{
    public sealed class ListTimeEntryTagsByTimeEntryQueryValidator
        : AbstractValidator<ListTimeEntryTagsByTimeEntryQuery>
    {
        public ListTimeEntryTagsByTimeEntryQueryValidator()
        {
            RuleFor(x => x.TimeEntryId)
                .GreaterThan(0)
                .WithMessage("Time entry ID is required.");
        }
    }
}