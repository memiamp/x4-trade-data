using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IGateData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class GateDataParser(
                              IDataParser dataParser,
                              ILogger<GateDataParser> logger)
    : DataParserBase<IGateData>(dataParser, logger)
{
    private protected override async Task<IGateData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.GateId, out string? id) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro))
        {
            Logger.LogWarning("Could not load gate");
            throw new ArgumentException("Could not load gate", nameof(reader));
        }

        var isKnown = GetIsKnownToPlayer(reader);

        var transform = await ParseElements(reader);

        return new GateData
        {
            Code = code,
            Id = id,
            IsKnown = isKnown,
            Macro = macro,
            Transform = transform
        };
    }

    private async Task<ITransform3D> ParseElements(IXmlReaderWrapper reader)
    {
        ITransform3D transform = ITransform3D.GetDefault();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                transform = await DataParser.Parse<ITransform3D>(subtree);
                break;
            }
        }

        return transform;
    }
}
