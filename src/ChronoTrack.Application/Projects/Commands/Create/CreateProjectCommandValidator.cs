using FluentValidation;

namespace ChronoTrack.Application.Projects.Commands.Create
{
    public sealed class CreateProjectCommandValidator
        : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .GreaterThan(0)
                .WithMessage("Workspace ID is required.");

            RuleFor(x => x.ClientId)
                .GreaterThan(0)
                .When(x => x.ClientId.HasValue)
                .WithMessage("Client ID must be greater than 0.");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Actor)
                .NotEmpty()
                .WithMessage("Actor is required.");
        }
    }
}