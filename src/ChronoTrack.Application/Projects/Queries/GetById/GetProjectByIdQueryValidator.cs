using FluentValidation;

namespace ChronoTrack.Application.Projects.Queries.GetById
{
    public sealed class GetProjectByIdQueryValidator
        : AbstractValidator<GetProjectByIdQuery>
    {
        public GetProjectByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Project ID is required.");
        }
    }
}