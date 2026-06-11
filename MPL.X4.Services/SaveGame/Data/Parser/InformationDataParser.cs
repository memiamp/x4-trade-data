using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IInformationData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class InformationDataParser(
                                     IDataParser dataParser,
                                     ILogger<InformationDataParser> logger)
    : DataParserBase<IInformationData>(dataParser, logger)
{
    private protected override async Task<IInformationData> OnParse(IXmlReaderWrapper reader)
    {
        IGameData? game = null;
        IPlayerData? player = null;
        ISaveData? save = null;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Game, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                game = await DataParser.Parse<IGameData>(subtree);
            }
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Player, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                player = await DataParser.Parse<IPlayerData>(subtree);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Save, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                save = await DataParser.Parse<ISaveData>(subtree);
            }
        }

        if (game is null ||
            player is null ||
            save is null)
        {
            logger.LogWarning("Could not load save game information");
            throw new ArgumentException("Could not load save game information", nameof(reader));
        }

        return new InformationData
        {
            Game = game,
            Player = player,
            Save = save
        };
    }
}
