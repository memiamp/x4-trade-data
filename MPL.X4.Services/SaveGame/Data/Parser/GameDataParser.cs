using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IGameData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class GameDataParser(
                              IDataParser dataParser,
                              ILogger<GameDataParser> logger)
    : DataParserBase<IGameData>(dataParser, logger)
{
    private protected override Task<IGameData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Time, out double? time))
        {
            logger.LogWarning("Could not load game");
            throw new ArgumentException("Could not load game", nameof(reader));
        }

        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Modified, out int? modified);

        var returnValue = new GameData
        {
            Modified = modified ?? 0,
            Time = time.Value
        };

        return Task.FromResult<IGameData>(returnValue);
    }
}
