namespace SimpleCare.Orderlies.Domain;

public record OrderlyTask(Guid Id, string StartLocation, string EndLocation, string Description, DateTimeOffset? StartTime, DateTimeOffset? EndTime)
{
    internal OrderlyTask StartTask()
    {
        return this with { StartTime = DateTimeOffset.UtcNow };
    }

    internal OrderlyTask EndTask()
    {
        return this with { EndTime = DateTimeOffset.UtcNow };
    }
}
