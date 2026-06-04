using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.ReadModels.Clients;
using MediatR;

namespace ChronoTrack.Application.Clients.Queries.GetById
{
    public sealed class GetClientByIdQueryHandler
        : IRequestHandler<GetClientByIdQuery, ClientReadModel>
    {
        private readonly IClientReadRepository _clientReadRepository;

        public GetClientByIdQueryHandler(IClientReadRepository clientReadRepository)
        {
            _clientReadRepository = clientReadRepository;
        }

        public async Task<ClientReadModel> Handle(
            GetClientByIdQuery query,
            CancellationToken ct)
        {
            var client = await _clientReadRepository.GetByIdAsync(query.Id, ct);

            if (client is null)
            {
                throw new NotFoundException("Client was not found.");
            }

            return client;
        }
    }
}