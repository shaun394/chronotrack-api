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
            object @event,
            CancellationToken ct = default)
        {
            _session.Events.Append(
                streamId,
                @event);

            await _session.SaveChangesAsync(ct);
        }
    }
}