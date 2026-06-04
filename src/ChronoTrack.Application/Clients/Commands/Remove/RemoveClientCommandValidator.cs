using FluentValidation;

namespace ChronoTrack.Application.Clients.Commands.Remove
{
    public sealed class RemoveClientCommandValidator
        : AbstractValidator<RemoveClientCommand>
    {
        public RemoveClientCommandValidator()
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