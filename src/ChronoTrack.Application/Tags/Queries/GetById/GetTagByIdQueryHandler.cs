using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Application.ReadModels.Tags;
using ChronoTrack.Domain.Tags;
using MediatR;

namespace ChronoTrack.Application.Tags.Queries.GetById
{
    public sealed class GetTagByIdQueryHandler
        : IRequestHandler<GetTagByIdQuery, TagReadModel>
    {
        private readonly ITagReadRepository _repository;

        public GetTagByIdQueryHandler(
            ITagReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<TagReadModel> Handle(
            GetTagByIdQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .GetByIdAsync(query.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(Tag),
                    query.Id,
                    nameof(GetTagByIdQueryHandler));
            }

            return result;
        }
    }
}