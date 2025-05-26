
using System.Collections.Immutable;

using SimpleCare.Orderlies.Domain.Interfaces;

namespace SimpleCare.Orderlies.Domain;

public class OrderlyRoot(IOrderlyTaskRepository orderlyTaskRepository) : IOrderly
{
    public Task<ImmutableList<OrderlyTask>> GetTasks(CancellationToken cancellationToken)
    {
        return orderlyTaskRepository.GetAll();
    }

    public Task RegisterNewTask(string startLocation, string endLocation, string Description, CancellationToken cancellationToken)
    {
        var task = new OrderlyTask(Guid.NewGuid(), startLocation, endLocation, Description, null, null);

        return orderlyTaskRepository.AddTask(task, cancellationToken);
    }

    public async Task StartTask(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await orderlyTaskRepository.GetTask(taskId, cancellationToken);

        task = task.StartTask();

        await orderlyTaskRepository.UpdateTask(task, cancellationToken);
    }

    public async Task EndTask(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await orderlyTaskRepository.GetTask(taskId, cancellationToken);

        task = task.EndTask();

        await orderlyTaskRepository.UpdateTask(task, cancellationToken);
    }
}
