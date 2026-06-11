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
        try
        {
            using var reader = xmlReaderWrapperFactory.CreateXmlReader(sourcePath);

            var returnValue = await dataParser.Parse<ISaveGameData>(reader);

            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Could not load save game from {SourcePath}", sourcePath);
            throw new ArgumentException($"Could not load save game from '{sourcePath}'", nameof(sourcePath), ex);
        }
    }
}
