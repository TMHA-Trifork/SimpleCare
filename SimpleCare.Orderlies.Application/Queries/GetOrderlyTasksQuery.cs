using System.Collections.Immutable;

using MediatR;

using SimpleCare.Orderlies.Application.Values;
using SimpleCare.Orderlies.Domain.Interfaces;

namespace SimpleCare.Orderlies.Application.Queries;

public record GetOrderlyTasksQuery : IRequest<IImmutableList<OrderlyTaskListItem>>;

public class GetOrderlyTasksQueryHandler(IOrderly orderlyRoot) : IRequestHandler<GetOrderlyTasksQuery, IImmutableList<OrderlyTaskListItem>>
{
    public async Task<IImmutableList<OrderlyTaskListItem>> Handle(GetOrderlyTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await orderlyRoot.GetTasks(cancellationToken);
        return [.. tasks.Select(t => OrderlyTaskListItem.Map(t))];
    }
}