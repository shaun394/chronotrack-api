using FluentValidation;

namespace ChronoTrack.Application.Tags.Queries.GetById
{
    public sealed class GetTagByIdQueryValidator
        : AbstractValidator<GetTagByIdQuery>
    {
        public GetTagByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Tag ID is required.");
        }
    }
}