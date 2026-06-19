using FluentValidation;

namespace ChronoTrack.Application.TimeEntries.Commands.Remove
{
    public sealed class RemoveTimeEntryCommandValidator
        : AbstractValidator<RemoveTimeEntryCommand>
    {
        public RemoveTimeEntryCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Time entry ID is required.");

            RuleFor(x => x.Actor)
                .NotEmpty()
                .WithMessage("Actor is required.");
        }
    }
}