using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

using LockboxElements = (
                         int LockCount,
                         ITransform3D Transform,
                         IEnumerable<IWareItemData> Wares);

/// <summary>
/// A class that implements a data parser for a <see cref="ILockboxData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class LockboxDataParser(
                                 IDataParser dataParser,
                                 ILogger<LockboxDataParser> logger)
    : DataParserBase<ILockboxData>(dataParser, logger)
{
    private protected override async Task<ILockboxData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.LockboxId, out string? id) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro))
        {
            Logger.LogWarning("Could not load lockbox");
            throw new ArgumentException("Could not load lockbox", nameof(reader));
        }

        var isKnown = GetIsKnownToPlayer(reader);

        var (lockCount, transform, wares) = await ParseElements(reader);

        return new LockboxData
        {
            Code = code,
            Id = id,
            IsKnown = isKnown,
            LockCount = lockCount,
            Macro = macro,
            Transform = transform,
            Wares = wares
        };
    }

    private async Task<LockboxElements> ParseElements(IXmlReaderWrapper reader)
    {
        var lockCount = 0;
        ITransform3D transform = ITransform3D.GetDefault();
        List<IWareItemData> wares = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                transform = await DataParser.Parse<ITransform3D>(subtree);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Wares, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<IEnumerable<IWareItemData>>(subtree);

                wares.AddRange(data);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, x => x == Constants.XmlDataFile.AttributeValue.Class.Lock))
            {
                lockCount++;
            }
        }

        return (lockCount, transform, wares);
    }

    //private static async Task<IEnumerable<IWareItemData>> ParseWares(IXmlReaderWrapper reader)
    //{
    //    var returnValue = new List<string>();

    //    while (await reader.ReadAsync())
    //    {
    //        if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Ware, XmlNodeType.Element, 1) &&
    //            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Ware, out string? ware))
    //        {
    //            returnValue.Add(ware);
    //        }
    //    }

    //    return returnValue;
    //}
}
