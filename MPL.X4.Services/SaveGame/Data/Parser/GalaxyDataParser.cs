using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IGalaxyData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class GalaxyDataParser(
                                IDataParser dataParser,
                                ILogger<GalaxyDataParser> logger)
    : DataParserBase<IGalaxyData>(dataParser, logger)
{
    private protected override async Task<IGalaxyData> OnParse(IXmlReaderWrapper reader)
    {
        var clusters = new List<IClusterData>();

        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.GalaxyId, out string? id) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro))
        {
            logger.LogWarning("Could not parse galaxy");
            throw new ArgumentException("Could not parse galaxy", nameof(reader));
        }

        Logger.LogInformation("Parsing galaxy {Macro} ({Code})", macro, code);

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Component, XmlNodeType.Element, 3) &&
                reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, x => x == Constants.XmlDataFile.AttributeValue.Class.Cluster))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<IClusterData>(subtree);

                clusters.Add(data);
            }
        }

        return new GalaxyData
        {
            Clusters = clusters,
            Code = code,
            Id = id,
            Macro = macro
        };
    }
}
