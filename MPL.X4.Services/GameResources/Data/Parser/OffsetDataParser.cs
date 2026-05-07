using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Models;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IOffsetData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="position3DParser">An <see cref="IDataParser{IPosition3D}"/> that is the position parser to use.</param>
/// <param name="rotation3DParser">An <see cref="IDataParser{IRotation3D}"/> that is the rotation parser to use.</param>
internal class OffsetDataParser(
                                ILogger<OffsetDataParser> logger,
                                IDataParser<IPosition3D> position3DParser,
                                IDataParser<IRotation3D> rotation3DParser)
    : DataParserBase<IOffsetData>(logger)
{
    private protected override async Task<IOffsetData> OnParse(IXmlReaderWrapper reader)
    {
        var sectorPosition = IPosition3D.GetDefault();
        var sectorRotation = IRotation3D.GetDefault();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Position, XmlNodeType.Element, 1))
            {
                sectorPosition = await position3DParser.Parse(reader);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Rotation, XmlNodeType.Element, 1))
            {
                sectorRotation = await rotation3DParser.Parse(reader);
            }
        }

        return new OffsetData
        {
            Position = sectorPosition,
            Rotation = sectorRotation
        };
    }
}
