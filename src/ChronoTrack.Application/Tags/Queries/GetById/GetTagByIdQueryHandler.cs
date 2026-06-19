using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Application.ReadModels.Tags;
using MediatR;

namespace ChronoTrack.Application.Tags.Queries.GetById
{
    public sealed class GetTagByIdQueryHandler
        : IRequestHandler<GetTagByIdQuery, TagReadModel>
    {
        private readonly ITagReadRepository _tagReadRepository;

        public GetTagByIdQueryHandler(
            ITagReadRepository tagReadRepository)
        {
            _tagReadRepository = tagReadRepository;
        }

        public async Task<TagReadModel> Handle(
            GetTagByIdQuery query,
            CancellationToken ct)
        {
            var tag = await _tagReadRepository
                .GetByIdAsync(query.Id, ct);

            if (tag is null)
            {
                throw new NotFoundException("Tag was not found.");
            }

            return tag;
        }
    }
}