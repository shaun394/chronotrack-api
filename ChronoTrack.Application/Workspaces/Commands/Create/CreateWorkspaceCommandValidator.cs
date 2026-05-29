using FluentValidation;

namespace ChronoTrack.Application.Workspaces.Commands.Create
{
    public sealed class CreateWorkspaceCommandValidator : AbstractValidator<CreateWorkspaceCommand>
    {
        public CreateWorkspaceCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Actor)
                .NotEmpty();
        }
    }
}