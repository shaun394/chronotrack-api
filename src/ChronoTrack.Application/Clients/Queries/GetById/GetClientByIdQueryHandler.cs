using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.ReadModels.Clients;
using ChronoTrack.Domain.Clients;
using MediatR;

namespace ChronoTrack.Application.Clients.Queries.GetById
{
    public sealed class GetClientByIdQueryHandler
        : IRequestHandler<GetClientByIdQuery, ClientReadModel>
    {
        private readonly IClientReadRepository _repository;

        public GetClientByIdQueryHandler(
            IClientReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClientReadModel> Handle(
            GetClientByIdQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .GetByIdAsync(query.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Client),
                    query.Id,
                    nameof(GetClientByIdQueryHandler));
            }

            return result;
        }
    }
}