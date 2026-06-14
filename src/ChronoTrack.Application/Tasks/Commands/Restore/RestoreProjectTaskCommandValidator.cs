using FluentValidation;

namespace ChronoTrack.Application.Tasks.Commands.Restore
{
    public sealed class RestoreProjectTaskCommandValidator
        : AbstractValidator<RestoreProjectTaskCommand>
    {
        public RestoreProjectTaskCommandValidator()
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