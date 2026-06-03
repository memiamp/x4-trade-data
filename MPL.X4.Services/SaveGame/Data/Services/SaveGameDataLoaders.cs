using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Services;

/// <summary>
/// A class that implements a loader of save game data.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="xmlReaderWrapperFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XmlReader factory to use.</param>
internal class SaveGameDataLoader(
                                  IDataParser dataParser,
                                  ILogger<SaveGameDataLoader> logger,
                                  IXmlReaderWrapperFactory xmlReaderWrapperFactory)
    : ISaveGameDataLoader
{
    async Task<ISaveGameData> ISaveGameDataLoader.LoadFrom(string sourcePath)
    {
        IEconomyLogData? economyLog = null;
        IUniverseData? universeData = null;

        using var reader = xmlReaderWrapperFactory.CreateXmlReader(sourcePath);

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Universe, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                universeData = await dataParser.Parse<IUniverseData>(subtree);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.EconomyLog, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                economyLog = await dataParser.Parse<IEconomyLogData>(subtree);
            }
        }

        if (universeData is null ||
            economyLog is null)
        {
            logger.LogWarning("Could not load save game from {SourcePath}", sourcePath);
            throw new ArgumentException($"Could not load save game from '{sourcePath}'", nameof(sourcePath));
        }

        return new SaveGameData
        {
            EconomyLog = economyLog,
            Universe = universeData
        };
    }
}
