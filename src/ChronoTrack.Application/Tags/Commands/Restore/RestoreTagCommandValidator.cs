using FluentValidation;

namespace ChronoTrack.Application.Tags.Commands.Restore
{
    public sealed class RestoreTagCommandValidator
        : AbstractValidator<RestoreTagCommand>
    {
        public RestoreTagCommandValidator()
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