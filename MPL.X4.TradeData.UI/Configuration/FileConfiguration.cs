namespace MPL.X4.TradeData.UI.Configuration;

/// <summary>
/// A class that implements the file configuration for the application.
/// </summary>
internal class FileConfiguration : IFileConfiguration
{
    string IFileConfiguration.CatalogFilePath => @"";

    uint IFileConfiguration.CheckIntervalSeconds => 10;

    uint IFileConfiguration.SaveGameFileAgeSeconds => 30;

    string IFileConfiguration.SaveGameFileFilter => "*.xml.gz";

    string IFileConfiguration.SaveGameFilePath => @"";
}
