using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for an <see cref="IOffset"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="sectorPositionParser">An <see cref="IDataParser{ISectorPosition}"/> that is the sector position parser to use.</param>
/// <param name="sectorRotationParser">An <see cref="IDataParser{ISectorRotation}"/> that is the sector rotation parser to use.</param>
internal class OffsetParser(
                            ILogger<OffsetParser> logger,
                            IDataParser<ISectorPosition> sectorPositionParser,
                            IDataParser<ISectorRotation> sectorRotationParser)
    : DataParserBase<IOffset>(logger)
{
    private protected override async Task<IOffset> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        ISectorPosition sectorPosition = ISectorPosition.GetDefault();
        ISectorRotation sectorRotation = ISectorRotation.GetDefault();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Position, XmlNodeType.Element, 1))
            {
                sectorPosition = await sectorPositionParser.Parse(reader);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Rotation, XmlNodeType.Element, 1))
            {
                sectorRotation = await sectorRotationParser.Parse(reader);
            }
        }

        return new Offset
        {
            Position = sectorPosition,
            Rotation = sectorRotation
        };
    }
}
