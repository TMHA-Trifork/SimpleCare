namespace SimpleCare.Infrastructure.UnitOfWork;

public class SqlServerSettings
{
    public required string ConnectionString { get; init; }
    public required int Timeout { get; init; }
}
