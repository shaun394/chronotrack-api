using ChronoTrack.Application.ReadModels.Projects;
using MediatR;

namespace ChronoTrack.Application.Projects.Queries.GetById
{
    public sealed record GetProjectByIdQuery(int Id) : IRequest<ProjectReadModel>;
}