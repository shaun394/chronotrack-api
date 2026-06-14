using FluentValidation;

namespace ChronoTrack.Application.Tasks.Queries.GetById
{
    public sealed class GetProjectTaskByIdQueryValidator
        : AbstractValidator<GetProjectTaskByIdQuery>
    {
        public GetProjectTaskByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Project task ID is required.");
        }
    }
}