using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntries.Commands.Update
{
    public sealed class UpdateTimeEntryCommandHandler
        : IRequestHandler<UpdateTimeEntryCommand, int>
    {
        private readonly ITimeEntryWriteRepository _timeEntryWriteRepository;
        private readonly IProjectReadRepository _projectReadRepository;
        private readonly IProjectTaskReadRepository _projectTaskReadRepository;
        private readonly IClientReadRepository _clientReadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTimeEntryCommandHandler(
            ITimeEntryWriteRepository timeEntryWriteRepository,
            IProjectReadRepository projectReadRepository,
            IProjectTaskReadRepository projectTaskReadRepository,
            IClientReadRepository clientReadRepository,
            IUnitOfWork unitOfWork)
        {
            _timeEntryWriteRepository = timeEntryWriteRepository;
            _projectReadRepository = projectReadRepository;
            _projectTaskReadRepository = projectTaskReadRepository;
            _clientReadRepository = clientReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            UpdateTimeEntryCommand command,
            CancellationToken ct)
        {
            var timeEntry = await _timeEntryWriteRepository
                .GetForUpdateAsync(command.Id, ct);

            if (timeEntry is null)
            {
                throw new NotFoundException("Time entry was not found.");
            }

            var project = await _projectReadRepository
                .GetByIdAsync(command.ProjectId, ct);

            if (project is null)
            {
                throw new NotFoundException("Project was not found.");
            }

            if (project.WorkspaceId != timeEntry.WorkspaceId)
            {
                throw new NotFoundException("Project was not found in the selected workspace.");
            }

            if (command.ProjectTaskId.HasValue)
            {
                var projectTask = await _projectTaskReadRepository
                    .GetByIdAsync(command.ProjectTaskId.Value, ct);

                if (projectTask is null)
                {
                    throw new NotFoundException("Project task was not found.");
                }

                if (projectTask.ProjectId != command.ProjectId)
                {
                    throw new NotFoundException("Project task was not found in the selected project.");
                }
            }

            if (command.ClientId.HasValue)
            {
                var client = await _clientReadRepository
                    .GetByIdAsync(command.ClientId.Value, ct);

                if (client is null)
                {
                    throw new NotFoundException("Client was not found.");
                }

                if (client.WorkspaceId != timeEntry.WorkspaceId)
                {
                    throw new NotFoundException("Client was not found in the selected workspace.");
                }
            }

            timeEntry.Update(
                command.ProjectId,
                command.ProjectTaskId,
                command.ClientId,
                command.Description,
                command.WorkDate,
                command.StartTime,
                command.EndTime,
                command.IsBillable,
                command.Actor,
                DateTimeOffset.UtcNow);

            await _unitOfWork.SaveChangesAsync(ct);

            return timeEntry.Id;
        }
    }
}