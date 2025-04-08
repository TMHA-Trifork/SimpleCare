using System.Collections.Immutable;

using MediatR;

using SimpleCare.BedWards.Boundary.Values;

namespace SimpleCare.BedWards.Boundary.Queries;

public record GetBedWardsQuery : IRequest<ImmutableList<BedWardItem>>;
