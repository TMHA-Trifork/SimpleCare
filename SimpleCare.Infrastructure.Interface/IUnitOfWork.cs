namespace SimpleCare.Infrastructure.Interface;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken cancellationToken);

}
