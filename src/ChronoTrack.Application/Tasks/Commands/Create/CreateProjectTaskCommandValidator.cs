using FluentValidation;

namespace ChronoTrack.Application.Tasks.Commands.Create
{
    public sealed class CreateProjectTaskCommandValidator
        : AbstractValidator<CreateProjectTaskCommand>
    {
        public CreateProjectTaskCommandValidator()
        {
            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("Project ID is required.");

            RuleFor(x => x.Name)
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