using ChronoTrack.Application.ReadModels.Clients;
using MediatR;

namespace ChronoTrack.Application.Clients.Queries.GetById
{
    public sealed record GetClientByIdQuery(int Id)
        : IRequest<ClientReadModel>;
}