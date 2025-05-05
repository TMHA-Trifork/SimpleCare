using System.Collections.Immutable;

namespace SimpleCare.Orderlies.Domain.Interfaces;

public interface IOrderlyTaskRepository
{
    Task<OrderlyTask> GetTask(Guid taskId, CancellationToken cancellationToken);

    Task<ImmutableList<OrderlyTask>> GetAll();

    Task AddTask(OrderlyTask task, CancellationToken cancellationToken);

    Task UpdateTask(OrderlyTask task, CancellationToken cancellationToken);
}
