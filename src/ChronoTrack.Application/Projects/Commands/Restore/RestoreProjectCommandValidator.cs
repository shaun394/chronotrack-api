using FluentValidation;

namespace ChronoTrack.Application.Projects.Commands.Restore
{
    public sealed class RestoreProjectCommandValidator
        : AbstractValidator<RestoreProjectCommand>
    {
        public RestoreProjectCommandValidator()
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