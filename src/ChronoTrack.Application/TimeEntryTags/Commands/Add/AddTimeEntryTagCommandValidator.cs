using FluentValidation;

namespace ChronoTrack.Application.TimeEntryTags.Commands.Add
{
    public sealed class AddTimeEntryTagCommandValidator
        : AbstractValidator<AddTimeEntryTagCommand>
    {
        public AddTimeEntryTagCommandValidator()
        {
            RuleFor(x => x.TimeEntryId)
                .GreaterThan(0)
                .WithMessage("Time entry ID is required.");

            RuleFor(x => x.TagId)
                .GreaterThan(0)
                .WithMessage("Tag ID is required.");

            RuleFor(x => x.Actor)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Actor is required.")
                .MaximumLength(100)
                .WithMessage("Actor cannot exceed 100 characters.");
        }
    }
}