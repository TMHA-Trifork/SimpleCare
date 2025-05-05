

using SimpleCare.Orderlies.Domain;

namespace SimpleCare.Orderlies.Application.Values;

public record OrderlyTaskListItem(Guid Id, string StartLocation, string EndLocation, string Description, DateTimeOffset? StartTime, DateTimeOffset? EndTime)
{
    internal static OrderlyTaskListItem Map(OrderlyTask t)
    {
        return new OrderlyTaskListItem(
            t.Id,
            t.StartLocation,
            t.EndLocation,
            t.Description,
            t.StartTime,
            t.EndTime);
    }
}
