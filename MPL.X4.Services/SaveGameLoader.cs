using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.DataParser;
using MPL.X4.Services.Models;

namespace MPL.X4.Services;

/// <summary>
/// A class that implements a loader of save games.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="universeParser">An <see cref="IDataParser{IUniverse}"/> that is the universe parser to use.</param>
/// <param name="xmlReaderFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XmlReader factory to use.</param>
internal class SaveGameLoader(
                              ILogger<SaveGameLoader> logger,
                              IDataParser<IUniverse> universeParser,
                              IXmlReaderWrapperFactory xmlReaderFactory)
    : ISaveGameLoader
{
    async Task<ISaveGame> ISaveGameLoader.LoadFrom(string sourcePath)
    {
        ISaveGame? returnValue = null;

        using var reader = xmlReaderFactory.CreateXmlReader(sourcePath);

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Universe, XmlNodeType.Element))
            {
                using var universeSubtree = await reader.ReadSubtree();

                var universe = await universeParser.Parse(universeSubtree);
                returnValue = new SaveGame
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
