using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Domain.Common;
using Marten;

namespace ChronoTrack.Infrastructure.EventStore
{
    public sealed class MartenEventStore : IEventStore
    {
        private readonly IDocumentSession _session;

        public MartenEventStore(IDocumentSession session)
        {
            _session = session;
        }

        public async Task AppendAsync(
            string streamId,
            DomainEvent domainEvent,
            CancellationToken ct)
        {
            _session.Events.Append(
                streamId,
                domainEvent);

            await _session.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyCollection<DomainEvent>> FetchStreamAsync(
            string streamId,
            CancellationToken ct)
        {
            var events = await _session.Events
                .FetchStreamAsync(streamId, token: ct);

            return events
                .Select(x => (DomainEvent)x.Data)
                .ToList();
        }
    }
}