using ChronoTrack.Domain.Common;

namespace ChronoTrack.Application.Common.Interfaces
{
    public interface IEventStore
    {
        Task AppendAsync(
            string streamId,
            DomainEvent domainEvent,
            CancellationToken ct);

        Task<IReadOnlyCollection<DomainEvent>> FetchStreamAsync(
            string streamId,
            CancellationToken ct);
    }
}