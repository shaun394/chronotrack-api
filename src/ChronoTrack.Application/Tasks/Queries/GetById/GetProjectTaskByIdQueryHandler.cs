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
        private readonly IProjectTaskReadRepository _repository;

        public GetProjectTaskByIdQueryHandler(
            IProjectTaskReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProjectTaskReadModel> Handle(
            GetProjectTaskByIdQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .GetByIdAsync(query.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(ProjectTask),
                    query.Id,
                    nameof(GetProjectTaskByIdQueryHandler));
            }

            return result;
        }
    }
}