using SimpleCare.Infrastructure.Interfaces.UnitOfWork;

namespace SimpleCare.Infrastructure.UnitOfWork
{
    public class EntityFrameworkUnitOfWork(SimpleCareDbContext dbContext) : IUnitOfWork
    {
        private bool disposedValue;

        public Task SaveChanges(CancellationToken cancellationToken)
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        ~EntityFrameworkUnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
