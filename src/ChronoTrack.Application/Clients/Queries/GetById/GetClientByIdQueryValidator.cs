using FluentValidation;

namespace ChronoTrack.Application.Clients.Queries.GetById
{
    public sealed class GetClientByIdQueryValidator
        : AbstractValidator<GetClientByIdQuery>
    {
        public GetClientByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Client ID is required.");
        }
    }
}