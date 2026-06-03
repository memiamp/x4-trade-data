namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a log entry.
/// </summary>
public interface ILogEntryData
{
    /// <summary>
    /// Gets the log entry time.
    /// </summary>
    decimal Time { get; }
}