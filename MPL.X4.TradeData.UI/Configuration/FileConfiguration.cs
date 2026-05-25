namespace MPL.X4.TradeData.UI.Configuration;

/// <summary>
/// A class that implements the file configuration for the application.
/// </summary>
internal class FileConfiguration : IFileConfiguration
{
    void IFileConfiguration.Save()
    {
        Properties.Settings.Default.Save();
    }

    string IFileConfiguration.CatalogFilePath
    {
        get => Properties.Settings.Default.CatalogFilePath;
        set => Properties.Settings.Default.CatalogFilePath = value;
    }
    
    uint IFileConfiguration.CheckIntervalSeconds
    {
        get => Properties.Settings.Default.CheckIntervalSeconds;
        set => Properties.Settings.Default.CheckIntervalSeconds = value;
    }

    uint IFileConfiguration.SaveGameFileAgeSeconds
    {
        get => Properties.Settings.Default.SaveGameFileAgeSeconds;
        set => Properties.Settings.Default.SaveGameFileAgeSeconds = value;
    }

    string IFileConfiguration.SaveGameFileFilter
    {
        get => Properties.Settings.Default.SaveGameFileFilter;
        set => Properties.Settings.Default.SaveGameFileFilter = value;
    }

    string IFileConfiguration.SaveGameFilePath
    {
        get => Properties.Settings.Default.SaveGameFilePath;
        set => Properties.Settings.Default.SaveGameFilePath = value;
    }
}
