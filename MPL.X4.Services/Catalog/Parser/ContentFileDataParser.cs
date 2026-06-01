global using ContentFileData = (string GamePack, System.Collections.Generic.IEnumerable<string> Dependencies);

using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.Catalog.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="ContentFileData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ContentFileDataParser(
                                     IDataParser dataParser,
                                     ILogger<ContentFileDataParser> logger)
    : DataParserBase<ContentFileData>(dataParser, logger)
{
    private protected override async Task<ContentFileData> OnParse(IXmlReaderWrapper reader)
    {
        IEnumerable<string> dependencies = [];
        string? gamePack = null;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ContentFile.ElementName.Content, System.Xml.XmlNodeType.Element, 0) &&
                reader.TryGetAttribute(Constants.ContentFile.AttributeName.ContentId, out gamePack))
            {
                using var subtree = await reader.ReadSubtree();

                dependencies = await ParseContentFile(subtree);

                break;
            }
        }

        gamePack ??= string.Empty;

        return (gamePack, dependencies);
    }

    private static async Task<IEnumerable<string>> ParseContentFile(IXmlReaderWrapper reader)
    {
        var returnValue = new List<string>();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.ContentFile.ElementName.Dependency, System.Xml.XmlNodeType.Element, 1) &&
                reader.TryGetAttribute(Constants.ContentFile.AttributeName.DependencyId, out string? dependencyId))
            {
                returnValue.Add(dependencyId);
            }
        }

        return returnValue;
    }
}
