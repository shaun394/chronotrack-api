using FluentValidation;

namespace ChronoTrack.Application.TimeEntries.Commands.Restore
{
    public sealed class RestoreTimeEntryCommandValidator
        : AbstractValidator<RestoreTimeEntryCommand>
    {
        public RestoreTimeEntryCommandValidator()
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