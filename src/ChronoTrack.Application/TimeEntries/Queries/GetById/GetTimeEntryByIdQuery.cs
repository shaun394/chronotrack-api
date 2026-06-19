using ChronoTrack.Application.ReadModels.TimeEntries;
using MediatR;

namespace ChronoTrack.Application.TimeEntries.Queries.GetById
{
    public sealed record GetTimeEntryByIdQuery(int Id) : IRequest<TimeEntryReadModel>;
}