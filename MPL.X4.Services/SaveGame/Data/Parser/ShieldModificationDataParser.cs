using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IShieldModificationData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ShieldModificationDataParser(
                                            IDataParser dataParser,
                                            ILogger<ShieldModificationDataParser> logger)
    : DocumentDataParserBase<IShieldModificationData>(dataParser, logger)
{
    private protected override Task<IShieldModificationData> OnParse(IXDocumentWrapper document)
    {
        if (!document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
        {
            logger.LogWarning("Could not parse shield modification");
            throw new ArgumentException("Could not parse shield modification", nameof(document));
        }

        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.Capacity, out double? capacity);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.RechargeDelay, out double? rechargeDelay);
        document.TryGetRootAttribute(Constants.XmlDataFile.AttributeName.RechargeRate, out double? rechargeRate);

        var returnValue = new ShieldModificationData
        {
            Capacity = capacity ?? 0,
            RechargeDelay = rechargeDelay ?? 0,
            RechargeRate = rechargeRate ?? 0,
            Ware = ware
        };

        return Task.FromResult<IShieldModificationData>(returnValue);
    }
}
