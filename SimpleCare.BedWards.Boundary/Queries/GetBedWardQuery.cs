using MediatR;

using SimpleCare.BedWards.Boundary.Values;

namespace SimpleCare.BedWards.Boundary.Queries;

public record GetBedWardQuery(Guid wardId) : IRequest<BedWardItem>;
