using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Application.ReadModels.Projects;
using MediatR;

namespace ChronoTrack.Application.Projects.Queries.GetById
{
    public sealed class GetProjectByIdQueryHandler
        : IRequestHandler<
            GetProjectByIdQuery,
            ProjectReadModel>
    {
        private readonly IProjectReadRepository _projectReadRepository;

        public GetProjectByIdQueryHandler(
            IProjectReadRepository projectReadRepository)
        {
            _projectReadRepository = projectReadRepository;
        }

        public async Task<ProjectReadModel> Handle(
            GetProjectByIdQuery query,
            CancellationToken ct)
        {
            var project = await _projectReadRepository
                .GetByIdAsync(query.Id, ct);

            if (project is null)
            {
                throw new NotFoundException("Project was not found.");
            }

            return project;
        }
    }
}