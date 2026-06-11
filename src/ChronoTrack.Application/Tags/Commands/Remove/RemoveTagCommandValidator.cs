using FluentValidation;

namespace ChronoTrack.Application.Tags.Commands.Remove
{
    public sealed class RemoveTagCommandValidator
        : AbstractValidator<RemoveTagCommand>
    {
        public RemoveTagCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Tag ID is required.");

            RuleFor(x => x.Actor)
                .NotEmpty()
                .WithMessage("Actor is required.");
        }
    }
}