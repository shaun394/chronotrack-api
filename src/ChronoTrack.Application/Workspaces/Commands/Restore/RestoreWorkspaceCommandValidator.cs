using FluentValidation;

namespace ChronoTrack.Application.Workspaces.Commands.Restore
{
    public sealed class RestoreWorkspaceCommandValidator
        : AbstractValidator<RestoreWorkspaceCommand>
    {
        public RestoreWorkspaceCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Actor)
                .NotEmpty();
        }
    }
}