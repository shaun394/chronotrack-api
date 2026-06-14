using FluentValidation;

namespace ChronoTrack.Application.Tasks.Commands.Remove
{
    public sealed class RemoveProjectTaskCommandValidator
        : AbstractValidator<RemoveProjectTaskCommand>
    {
        public RemoveProjectTaskCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Project task ID is required.");

            RuleFor(x => x.Actor)
                .NotEmpty()
                .WithMessage("Actor is required.");
        }
    }
}