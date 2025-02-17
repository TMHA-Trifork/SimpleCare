namespace SimpleCare.Infrastructure.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task SaveChanges(CancellationToken cancellationToken);
}
