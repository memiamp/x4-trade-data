using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Models;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IZoneOffseDatat"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="offsetParser">An <see cref="IDataParser{IOffset}"/> that is the offset parser to use.</param>
internal class ZoneOffsetDataParser(
                                ILogger<ZoneOffsetDataParser> logger,
                                IDataParser<IOffsetData> offsetParser)
    : DataParserBase<IZoneOffsetData>(logger)
{
    private protected override async Task<IZoneOffsetData> OnParse(IXmlReaderWrapper reader)
    {
        var macro = string.Empty;
        var position = IPosition3D.GetDefault();
        var rotation = IRotation3D.GetDefault();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                var subtree = await reader.ReadSubtree();

                var offset = await offsetParser.Parse(subtree);

                position = offset.Position;
                rotation = offset.Rotation;
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Macro, XmlNodeType.Element, 1) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Ref, out string? reference))
            {
                macro = reference.ToLower();
            }
        }

        return new ZoneOffsetData
        {
            MacroName = macro,
            Position = position,
            Rotation = rotation
        };
    }
}
