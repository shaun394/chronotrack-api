using FluentValidation;

namespace ChronoTrack.Application.TimeEntries.Commands.Update
{
    public sealed class UpdateTimeEntryCommandValidator
        : AbstractValidator<UpdateTimeEntryCommand>
    {
        public UpdateTimeEntryCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Time entry ID is required.");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("Project ID is required.");

            RuleFor(x => x.ProjectTaskId)
                .GreaterThan(0)
                .When(x => x.ProjectTaskId.HasValue)
                .WithMessage("Project task ID must be greater than 0.");

            RuleFor(x => x.ClientId)
                .GreaterThan(0)
                .When(x => x.ClientId.HasValue)
                .WithMessage("Client ID must be greater than 0.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime)
                .WithMessage("End time must be after start time.");

            RuleFor(x => x.Actor)
                .NotEmpty()
                .WithMessage("Actor is required.");
        }
    }
}