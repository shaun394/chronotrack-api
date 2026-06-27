using ChronoTrack.Application.Common.Exceptions;
using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Application.ReadModels.TimeEntries;
using ChronoTrack.Domain.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntries.Queries.GetById
{
    public sealed class GetTimeEntryByIdQueryHandler
        : IRequestHandler<GetTimeEntryByIdQuery, TimeEntryReadModel>
    {
        private readonly ITimeEntryReadRepository _repository;

        public GetTimeEntryByIdQueryHandler(
            ITimeEntryReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<TimeEntryReadModel> Handle(
            GetTimeEntryByIdQuery query,
            CancellationToken ct)
        {
            var result = await _repository
                .GetByIdAsync(query.Id, ct);

            if (result is null)
            {
                throw new NotFoundException(
                    nameof(TimeEntry),
                    query.Id,
                    nameof(GetTimeEntryByIdQueryHandler));
            }

            return result;
        }
    }
}