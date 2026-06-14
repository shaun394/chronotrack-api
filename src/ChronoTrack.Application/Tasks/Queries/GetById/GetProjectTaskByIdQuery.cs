using ChronoTrack.Application.ReadModels.Tasks;
using MediatR;

namespace ChronoTrack.Application.Tasks.Queries.GetById
{
    public sealed record GetProjectTaskByIdQuery(int Id) : IRequest<ProjectTaskReadModel>;
}