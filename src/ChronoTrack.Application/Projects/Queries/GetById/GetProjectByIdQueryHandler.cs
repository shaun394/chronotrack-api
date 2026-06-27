using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Application.ReadModels.Projects;
using ChronoTrack.Domain.Projects;
using MediatR;

namespace ChronoTrack.Application.Projects.Queries.GetById
{
    public sealed class GetProjectByIdQueryHandler
        : IRequestHandler<GetProjectByIdQuery, ProjectReadModel>
    {
        private readonly IProjectReadRepository _repository;

        public GetProjectByIdQueryHandler(
            IProjectReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProjectReadModel> Handle(
            GetProjectByIdQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .GetByIdAsync(query.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Project),
                    query.Id,
                    nameof(GetProjectByIdQueryHandler));
            }

            return result;
        }
    }
}