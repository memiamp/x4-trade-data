using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IOffsetDataDictionary"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class OffsetDataDictionaryParser(
                                          IDataParser dataParser,
                                          ILogger<OffsetDataDictionaryParser> logger)
    : DataParserBase<IOffsetDataDictionary>(dataParser, logger)
{
    private async Task<IOffsetData?> ReadOffsetData(IXmlReaderWrapper reader)
    {
        IOffsetData? returnValue = null;

        if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Connection, XmlNodeType.Element) &&
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Name, out string? name) &&
            reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Reference, out string? referenceType))
        {
            string? macro = null;
            ITransform3D? offset = null;

            while (await reader.ReadAsync())
            {
                if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Offset, XmlNodeType.Element, 1))
                {
                    var subtree = await reader.ReadSubtree();

                    offset = await DataParser.Parse<ITransform3D>(subtree);
                }
                else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Macro, XmlNodeType.Element, 1) &&
                         reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Reference, out string? reference))
                {
                    macro = reference.ToLower();
                }
            }

            if (offset is not null &&
                !string.IsNullOrWhiteSpace(macro))
            {
                returnValue = new OffsetData
                {
                    Macro = macro,
                    Name = name,
                    Offset = offset,
                    ReferenceType = referenceType
                };
            }
        }

        return returnValue;
    }
    
    private protected override async Task<IOffsetDataDictionary> OnParse(IXmlReaderWrapper reader)
    {
        OffsetDataDictionary returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Connection, XmlNodeType.Element))
            {
                using var subtree = await reader.ReadSubtree(true);

                var data = await ReadOffsetData(subtree);
                if (!string.IsNullOrWhiteSpace(data?.Name))
                {
                    returnValue[data.Name] = data;
                }
            }
        }

        return returnValue;
    }
}
