using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IPaintModificationData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class PaintModificationDataParser(
                                           IDataParser dataParser,
                                           ILogger<PaintModificationDataParser> logger)
    : DocumentDataParserBase<IPaintModificationData>(dataParser, logger)
{
    private protected override Task<IPaintModificationData> OnParse(IXDocumentWrapper document)
    {
        if (!document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
        {
            logger.LogWarning("Could not parse paint modification");
            throw new ArgumentException("Could not parse paint modification", nameof(document));
        }

        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Generated, out int? generated);

        var returnValue = new PaintModificationData
        {
            Generated = generated == 1,
            Ware = ware
        };

        return Task.FromResult<IPaintModificationData>(returnValue);
    }
}
