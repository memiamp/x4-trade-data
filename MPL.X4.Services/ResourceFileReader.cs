using System.Xml;
using Microsoft.Extensions.Logging;

namespace MPL.X4.Services;

/// <summary>
/// A class that implements a resource file reader.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="xmlReaderFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XmlReader factory to use.</param>
internal class ResourceFileReader(
                                  ILogger<ResourceFileReader> logger,
                                  IXmlReaderWrapperFactory xmlReaderFactory)
    : IResourceFileReader
{
    private static async Task<Dictionary<int, string>> ReadPage(IXmlReaderWrapper reader)
    {
        var returnValue = new Dictionary<int, string>();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.TextEntry, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.TextId, out int? textId))
            {
                using var subtree = reader.ReadSubtree();

                await subtree.ReadAsync(); // move to start
                string value = await subtree.ReadElementContentAsStringAsync();

                returnValue.Add(textId.Value, value);
                //var value = await reader.ReadInnerXmlAsync();
                //var value = await reader.ReadElementContentAsStringAsync();
                //returnValue.Add(textId.Value, value);
            }
        }

        return returnValue;
    }

    async Task<Dictionary<int, string>> IResourceFileReader.ReadPage(string sourcePath, int pageId)
    {
        var results = await ((IResourceFileReader)this).ReadPages(sourcePath, pageId);
        return results[pageId];
    }

    async Task<Dictionary<int, Dictionary<int, string>>> IResourceFileReader.ReadPages(string sourcePath, params int[] pageIds)
    {
        Dictionary<int, Dictionary<int, string>> returnValue = [];

        logger.LogInformation("Reading resource pages from {SourcePath}", sourcePath);

        using var reader = xmlReaderFactory.CreateXmlReader(sourcePath);

        while (await reader.ReadAsync())
        {
            // Check that this is a page node and is required
            if (reader.CheckNodeMatches(Constants.ResourceFile.ElementName.Page, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.ResourceFile.AttributeName.PageId, out int? pageId) &&
                pageIds.Contains(pageId.Value))
            {
                using var pageSubtree = reader.ReadSubtree();

                var pageResults = await ReadPage(pageSubtree);

                returnValue.Add(pageId.Value, pageResults);
            }
        }

        return returnValue;
    }
}
