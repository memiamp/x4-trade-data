namespace MPL.X4.TradeData.UI.Configuration;

/// <summary>
/// An interface that defines the file configuration for the application.
/// </summary>
internal interface IFileConfiguration
{
    /// <summary>
    /// Saves the current settings from the configuration.
    /// </summary>
    void Save();

    /// <summary>
    /// Gets the path to the X4 catalog files.
    /// </summary>
    string CatalogFilePath { get; set; }

    /// <summary>
    /// Gets the number of seconds between checks for updates.
    /// </summary>
    uint CheckIntervalSeconds { get; set; }

    /// <summary>
    /// Gets the file age, in seconds, before an updated save game can be processed.
    /// </summary>
    uint SaveGameFileAgeSeconds { get; set; }

    /// <summary>
    /// Gets the filter used to identify save game files.
    /// </summary>
    string SaveGameFileFilter { get; set; }

    /// <summary>
    /// Gets the path to the X4 save files.
    /// </summary>
    string SaveGameFilePath { get; set; }
}
