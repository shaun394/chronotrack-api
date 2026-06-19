using FluentValidation;

namespace ChronoTrack.Application.Workspaces.Commands.Remove
{
    public sealed class RemoveWorkspaceCommandValidator
        : AbstractValidator<RemoveWorkspaceCommand>
    {
        public RemoveWorkspaceCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Actor)
                .NotEmpty();
        }
    }
}