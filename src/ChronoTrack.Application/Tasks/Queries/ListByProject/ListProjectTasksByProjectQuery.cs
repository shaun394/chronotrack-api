using ChronoTrack.Application.ReadModels.Tasks;
using MediatR;

namespace ChronoTrack.Application.Tasks.Queries.ListByProject
{
    public sealed record ListProjectTasksByProjectQuery(int ProjectId)
        : IRequest<IReadOnlyCollection<ProjectTaskReadModel>>;
}