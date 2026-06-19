using FluentValidation;

namespace ChronoTrack.Application.TimeEntries.Queries.GetById
{
    public sealed class GetTimeEntryByIdQueryValidator
        : AbstractValidator<GetTimeEntryByIdQuery>
    {
        public GetTimeEntryByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Time entry ID is required.");
        }
    }
}