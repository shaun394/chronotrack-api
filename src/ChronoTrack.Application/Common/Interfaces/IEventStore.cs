using ChronoTrack.Domain.Common;

namespace ChronoTrack.Application.Common.Interfaces
{
    public interface IEventStore
    {
        Task AppendAsync(
            string streamId,
            object @event,
            CancellationToken ct);
    }
}