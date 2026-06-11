using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ISaveGameData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class SaveGameDataParser(
                                  IDataParser dataParser,
                                  ILogger<SaveGameDataParser> logger)
    : DataParserBase<ISaveGameData>(dataParser, logger)
{
    private protected override async Task<ISaveGameData> OnParse(IXmlReaderWrapper reader)
    {
        IEconomyLogData? economyLog = null;
        IInformationData? information = null;
        IUniverseData? universe = null;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.EconomyLog, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                economyLog = await DataParser.Parse<IEconomyLogData>(subtree);
            }
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Information, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                information = await DataParser.Parse<IInformationData>(subtree);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Universe, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                universe = await DataParser.Parse<IUniverseData>(subtree);
            }

            // Exit early if everything obtained
            if (economyLog is not null &&
                information is not null &&
                universe is not null)
            {
                break;
            }
        }

        if (economyLog is null||
            information is null ||
            universe is null)
        {
            logger.LogWarning("Could not load save game. Has Economy Log: {HasEconomyLog}, Has Information: {HasInformation}, Has Universe: {HasUniverse}", economyLog is not null, information is not null, universe is not null);
            throw new ArgumentException("Could not load save game", nameof(reader));
        }

        return new SaveGameData
        {
            EconomyLog = economyLog,
            Information = information,
            Universe = universe
        };
    }
}
