namespace MPL.X4.TradeData.UI.Configuration;

/// <summary>
/// An interface that defines the file configuration for the application.
/// </summary>
internal interface IFileConfiguration
{
    /// <summary>
    /// Gets the path to the X4 catalog files.
    /// </summary>
    string CatalogFilePath { get; }

    /// <summary>
    /// Gets the number of seconds between checks for updates.
    /// </summary>
    uint CheckIntervalSeconds { get; }

    /// <summary>
    /// Gets the file age, in seconds, before an updated save game can be processed.
    /// </summary>
    uint SaveGameFileAgeSeconds { get; }

    /// <summary>
    /// Gets the filter used to identify save game files.
    /// </summary>
    string SaveGameFileFilter { get; }

    /// <summary>
    /// Gets the path to the X4 save files.
    /// </summary>
    string SaveGameFilePath { get; }
}
