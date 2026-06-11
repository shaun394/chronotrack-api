using FluentValidation;

namespace ChronoTrack.Application.Tags.Commands.Create
{
    public sealed class CreateTagCommandValidator
        : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .GreaterThan(0)
                .WithMessage("Workspace ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(50)
                .WithMessage("Name cannot exceed 50 characters.");

            RuleFor(x => x.Actor)
                .NotEmpty()
                .WithMessage("Actor is required.");
        }
    }
}