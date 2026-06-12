using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IPlayerData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class PlayerDataParser(
                                IDataParser dataParser,
                                ILogger<PlayerDataParser> logger)
    : DataParserBase<IPlayerData>(dataParser, logger)
{
    private protected override Task<IPlayerData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Location, out string? locationText) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Money, out double? money) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Name, out string? name) ||
            !TextResourceReference.TryParse(locationText, out var location))
        {
            logger.LogWarning("Could not parse player");
            throw new ArgumentException("Could not parse player", nameof(reader));
        }

        var returnValue = new PlayerData
        {
            Location = location,
            Money = (long)money,
            Name = name
        };

        return Task.FromResult<IPlayerData>(returnValue);
    }
}
