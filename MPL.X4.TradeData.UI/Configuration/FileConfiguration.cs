namespace MPL.X4.TradeData.UI.Configuration;

/// <summary>
/// A class that implements the file configuration for the application.
/// </summary>
internal class FileConfiguration : IFileConfiguration
{
    string IFileConfiguration.CatFilePath => @"C:\Program Files (x86)\Steam\steamapps\common\X4 Foundations";

    uint IFileConfiguration.CheckIntervalSeconds => 10;

    uint IFileConfiguration.SaveGameFileAgeSeconds => 30;

    string IFileConfiguration.SaveGameFileFilter => " *.xml.gz";

    string IFileConfiguration.SaveGameFilePath => @"C:\Users\martin\Documents\Egosoft\X4\48359014\save";

    string IFileConfiguration.TextResourceFilePath => @"C:\Users\martin\Desktop\_X4\0001-l044.xml";
}
