using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using ChronoTrack.Application.ReadModels.Tasks;
using ChronoTrack.Domain.Tasks;
using MediatR;

namespace ChronoTrack.Application.Tasks.Queries.GetById
{
    public sealed class GetProjectTaskByIdQueryHandler
        : IRequestHandler<GetProjectTaskByIdQuery, ProjectTaskReadModel>
    {
        private readonly IProjectTaskReadRepository _projectTaskReadRepository;

        public GetProjectTaskByIdQueryHandler(
            IProjectTaskReadRepository projectTaskReadRepository)
        {
            _projectTaskReadRepository = projectTaskReadRepository;
        }

        public async Task<ProjectTaskReadModel> Handle(
            GetProjectTaskByIdQuery query,
            CancellationToken ct)
        {
            var projectTask = await _projectTaskReadRepository
                .GetByIdAsync(query.Id, ct);

            if (projectTask is null)
            {
                throw new NotFoundException(
                    nameof(ProjectTask),
                    query.Id,
                    nameof(GetProjectTaskByIdQueryHandler));
            }

            return projectTask;
        }
    }
}