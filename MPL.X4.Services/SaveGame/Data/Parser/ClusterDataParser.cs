using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IClusterData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ClusterDataParser(
                                 IDataParser dataParser,
                                 ILogger<ClusterDataParser> logger)
    : DataParserBase<IClusterData>(dataParser, logger)
{
    private protected override async Task<IClusterData> OnParse(IXmlReaderWrapper reader)
    {
        List<IHighwayData> highways = [];
        List<ISectorData> sectors = [];

        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.ClusterId, out string? id) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro))
        {
            logger.LogWarning("Could not parse cluster");
            throw new ArgumentException("Could not parse cluster", nameof(reader));
        }

        Logger.LogInformation("Parsing cluster {Macro} ({Code})", macro, code);

        var isKnown = GetIsKnownToPlayer(reader);

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element, 3) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, out string? className))
            {
                if (className == Constants.XmlDataFile.AttributeValue.Class.Highway)
                {
                    await ParseSubtree<IHighwayData>(reader, highways);
                }
                else if (className == Constants.XmlDataFile.AttributeValue.Class.Sector)
                {
                    await ParseSubtree<ISectorData>(reader, sectors);
                }
            }
        }

        return new ClusterData
        {
            Code = code,
            Highways = highways,
            Id = id,
            IsKnown = isKnown,
            Macro = macro,
            Sectors = sectors
        };
    }
}
