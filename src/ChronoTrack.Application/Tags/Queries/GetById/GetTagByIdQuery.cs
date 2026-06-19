using ChronoTrack.Application.ReadModels.Tags;
using MediatR;

namespace ChronoTrack.Application.Tags.Queries.GetById
{
    public sealed record GetTagByIdQuery(
        int Id) : IRequest<TagReadModel>;
}