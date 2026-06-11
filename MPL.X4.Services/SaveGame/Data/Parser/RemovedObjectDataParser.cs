using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IRemovedObjectData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class RemovedObjectDataParser(
                                       IDataParser dataParser,
                                       ILogger<RemovedObjectDataParser> logger)
    : DataParserBase<IRemovedObjectData>(dataParser, logger)
{
    private protected override Task<IRemovedObjectData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.RemovedObjectId, out string? id) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Name, out string? name) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Owner, out string? owner) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Space, out string? space))
        {
            logger.LogWarning("Could not load removed object");
            throw new ArgumentException("Could not load removed object", nameof(reader));
        }

        var returnValue = new RemovedObjectData
        {
            Code = code,
            Id = id,
            Name = name,
            Owner = owner,
            Space = space
        };

        return Task.FromResult<IRemovedObjectData>(returnValue);
    }
}
