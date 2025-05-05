using System.Collections.Immutable;

using Microsoft.EntityFrameworkCore;

using SimpleCare.Infrastructure.UnitOfWork;
using SimpleCare.Orderlies.Domain;
using SimpleCare.Orderlies.Domain.Interfaces;

namespace SimpleCare.Infrastructure.Orderlies;

public class OrderlyTaskRepository(SimpleCareDbContext dbContext) : IOrderlyTaskRepository
{
    private readonly DbSet<OrderlyTask> tasks = dbContext.Set<OrderlyTask>();

    public async Task<OrderlyTask> GetTask(Guid taskId, CancellationToken cancellationToken)
    {
        var task = (await tasks.FindAsync(taskId, cancellationToken))
            ?? throw new Exception($"OrderlyTask with id={taskId} not found");

        return task;
    }

    public async Task<ImmutableList<OrderlyTask>> GetAll()
    {
        return [.. await tasks.ToListAsync()];
    }

    public async Task AddTask(OrderlyTask task, CancellationToken cancellationToken)
    {
        await tasks.AddAsync(task, cancellationToken);
    }

    public async Task UpdateTask(OrderlyTask task, CancellationToken cancellationToken)
    {
        var t = await GetTask(task.Id, cancellationToken);
        tasks.Entry(t).CurrentValues.SetValues(task);
    }
}
