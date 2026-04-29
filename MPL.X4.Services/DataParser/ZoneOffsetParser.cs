using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for an <see cref="IZoneOffset"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="offsetParser">An <see cref="IDataParser{IOffset}"/> that is the offset parser to use.</param>
internal class ZoneOffsetParser(
                                ILogger<ZoneOffsetParser> logger,
                                IDataParser<IOffset> offsetParser)
    : DataParserBase<IZoneOffset>(logger)
{
    private protected override async Task<IZoneOffset> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        var macro = string.Empty;
        ISectorPosition position = ISectorPosition.GetDefault();
        ISectorRotation rotation = ISectorRotation.GetDefault();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                var offsetSubtree = await reader.ReadSubtree();

                var offset = await offsetParser.Parse(offsetSubtree);

                position = offset.Position;
                rotation = offset.Rotation;
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Macro, XmlNodeType.Element, 1) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Ref, out string? reference))
            {
                macro = reference.ToLower();
            }
        }

        return new ZoneOffset
        {
            MacroName = macro,
            Position = position,
            Rotation = rotation
        };
    }
}
