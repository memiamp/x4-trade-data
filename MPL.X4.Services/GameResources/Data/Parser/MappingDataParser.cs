using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IMappingData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class MappingDataParser(
                                 ILogger<MappingDataParser> logger)
    : DataParserBase<IMappingData>(logger)
{
    private protected override Task<IMappingData> OnParse(IXmlReaderWrapper reader)
    {
        MappingData? returnValue;

        if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Mapping, XmlNodeType.Element) &&
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.MappingId, out string? id) &&
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Reference, out string? reference))
        {
            returnValue = new MappingData
            {
                Id = id,
                Reference = reference
            };
        }
        else
        {
            logger.LogWarning("Could not load mapping");
            throw new ArgumentException("Could not load mapping", nameof(reader));
        }

        return Task.FromResult<IMappingData>(returnValue);
    }
}
