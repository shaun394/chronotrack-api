using FluentValidation;

namespace ChronoTrack.Application.Workspaces.Commands.Update
{
    public sealed class UpdateWorkspaceCommandValidator : AbstractValidator<UpdateWorkspaceCommand>
    {
        public UpdateWorkspaceCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Actor)
                .NotEmpty();
        }
    }
}