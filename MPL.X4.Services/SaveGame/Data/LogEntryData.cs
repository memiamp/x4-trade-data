namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a log entry.
/// </summary>
internal class LogEntryData : ILogEntryData
{
    public override string ToString()
        => $"Time {Time}";

    public required decimal Time { get; init; }
}
