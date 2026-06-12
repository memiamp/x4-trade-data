using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ISaveData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class SaveDataParser(
                              IDataParser dataParser,
                              ILogger<SaveDataParser> logger)
    : DataParserBase<ISaveData>(dataParser, logger)
{
    private protected override Task<ISaveData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Date, out double? date) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Name, out string? name))
        {
            logger.LogWarning("Could not parse save");
            throw new ArgumentException("Could not parse save", nameof(reader));
        }

        var returnValue = new SaveData
        {
            Date = (decimal)date,
            Name= name
        };

        return Task.FromResult<ISaveData>(returnValue);
    }
}
