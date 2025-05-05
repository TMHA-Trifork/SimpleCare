using System.Collections.Immutable;

namespace SimpleCare.Orderlies.Domain.Interfaces;

public interface IOrderly
{
    Task<ImmutableList<OrderlyTask>> GetTasks(CancellationToken cancellationToken);

    Task RegisterNewTask(string startLocation, string endLocation, string Description, CancellationToken cancellationToken);
    Task StartTask(Guid taskId, CancellationToken cancellationToken);
    Task EndTask(Guid taskId, CancellationToken cancellationToken);
}
