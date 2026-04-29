using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.DataParser;

namespace MPL.X4.Services;

/// <summary>
/// A class that implements a resource file reader.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="xmlReaderFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XmlReader factory to use.</param>
/// <param name="zoneOffsetParser">An <see cref="IDataParser{IZoneOffset}"/> that is the zone offset parser to use.</param>
internal class ResourceFileReader(
                                  ILogger<ResourceFileReader> logger,
                                  IXmlReaderWrapperFactory xmlReaderFactory,
                                  IDataParser<IZoneOffset> zoneOffsetParser)
    : IResourceFileReader
{
    private static async Task<string> ReadDatasetName(IXmlReaderWrapper reader)
    {
        var returnValue = string.Empty;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Identification, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.Name, out string? name))
            {
                returnValue = name;
                break;
            }
        }

        return returnValue;
    }

    private static async Task<Dictionary<int, string>> ReadTextResourceInternal(IXmlReaderWrapper reader)
    {
        var returnValue = new Dictionary<int, string>();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.TextEntry, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.TextId, out int? textId))
            {
                using var subtree = await reader.ReadSubtree();

                string value = await subtree.ReadElementContentAsStringAsync();

                returnValue.Add(textId.Value, value);
            }
        }

        return returnValue;
    }

    private static async Task<Dictionary<int, Dictionary<int, string>>> ReadTextResourceInternal(IXmlReaderWrapper reader, int[]? pageIds)
    {
        Dictionary<int, Dictionary<int, string>> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Page, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.PageId, out int? pageId) &&
                (pageIds is null ||
                pageIds?.Contains(pageId.Value) == true))
            {
                using var pageSubtree = await reader.ReadSubtree();

                var pageResults = await ReadTextResourceInternal(pageSubtree);

                returnValue.Add(pageId.Value, pageResults);
            }
        }

        return returnValue;
    }

    async Task<Dictionary<string, string>> IResourceFileReader.ReadMapDataset(IXmlReaderWrapper reader)
    {
        Dictionary<string, string> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Dataset, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.Macro, out string? macro))
            {
                using var datasetSubtree = await reader.ReadSubtree();

                macro = macro.ToLower();

                if (!returnValue.ContainsKey(macro))
                {
                    var name = await ReadDatasetName(datasetSubtree);
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        returnValue.Add(macro, name);
                    }
                }
            }
        }

        return returnValue;
    }
    
    Task<Dictionary<int, Dictionary<int, string>>> IResourceFileReader.ReadTextResource(IXmlReaderWrapper reader, params int[] pageIds)
        => ReadTextResourceInternal(reader, pageIds);

    Task<Dictionary<int, Dictionary<int, string>>> IResourceFileReader.ReadTextResource(IXmlReaderWrapper reader)
        => ReadTextResourceInternal(reader, null);

    async Task<Dictionary<int, string>> IResourceFileReader.ReadTextResource(string sourcePath, int pageId)
    {
        var results = await ((IResourceFileReader)this).ReadTextResource(sourcePath, [pageId]);
        return results[pageId];
    }

    async Task<Dictionary<int, Dictionary<int, string>>> IResourceFileReader.ReadTextResource(string sourcePath, params int[] pageIds)
    {
        logger.LogInformation("Reading resource pages from {SourcePath}", sourcePath);

        using var reader = xmlReaderFactory.CreateXmlReader(sourcePath);

        return await ReadTextResourceInternal(reader, pageIds);
    }

    async Task<Dictionary<string, IZoneOffset>> IResourceFileReader.ReadZoneOffset(IXmlReaderWrapper reader)
    {
        Dictionary<string, IZoneOffset> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Connection, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.Ref, x => x == Constants.ResourceFile.AttributeValue.Ref.Zones, out string? _))
            {
                using var zoneSubtree = await reader.ReadSubtree();

                var zoneOffset = await zoneOffsetParser.Parse(zoneSubtree);

                returnValue.TryAdd(zoneOffset.MacroName, zoneOffset);
            }
        }

        return returnValue;
    }
}
