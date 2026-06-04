using FluentValidation;

namespace ChronoTrack.Application.Clients.Commands.Restore
{
    public sealed class RestoreClientCommandValidator
        : AbstractValidator<RestoreClientCommand>
    {
        public RestoreClientCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Client ID is required.");

            RuleFor(x => x.Actor)
                .NotEmpty()
                .WithMessage("Actor is required.");
        }
    }
}