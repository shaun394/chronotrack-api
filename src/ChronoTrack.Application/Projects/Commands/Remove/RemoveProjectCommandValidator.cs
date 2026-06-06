using FluentValidation;

namespace ChronoTrack.Application.Projects.Commands.Remove
{
    public sealed class RemoveProjectCommandValidator
        : AbstractValidator<RemoveProjectCommand>
    {
        public RemoveProjectCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Project ID is required.");

            RuleFor(x => x.Actor)
                .NotEmpty()
                .WithMessage("Actor is required.");
        }
    }
}