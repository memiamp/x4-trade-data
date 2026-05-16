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
/// <param name="xmlReaderFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XmlReader factory to use.</param>
internal class SaveGameDataLoader(
                                  IDataParser dataParser,
                                  ILogger<SaveGameDataLoader> logger,
                                  IXmlReaderWrapperFactory xmlReaderFactory)
    : ISaveGameDataLoader
{
    async Task<ISaveGameData> ISaveGameDataLoader.LoadFrom(string sourcePath)
    {
        ISaveGameData? returnValue = null;

        using var reader = xmlReaderFactory.CreateXmlReader(sourcePath);

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Universe, XmlNodeType.Element))
            {
                using var universeSubtree = await reader.ReadSubtree();

                var universe = await dataParser.Parse<IUniverseData>(universeSubtree);
                returnValue = new SaveGameData
                {
                    Universe = universe
                };

                break;
            }
        }

        if (returnValue is null)
        {
            logger.LogWarning("Could not load save game from {SourcePath}", sourcePath);
            throw new ArgumentException($"Could not load save game from '{sourcePath}'", nameof(sourcePath));
        }

        return returnValue;
    }
}
