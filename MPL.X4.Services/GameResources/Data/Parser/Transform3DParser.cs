using System.Numerics;
using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="ITransform3D"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class Transform3DParser(
                                 IDataParser dataParser,
                                 ILogger<Transform3DParser> logger)
    : DataParserBase<ITransform3D>(dataParser, logger)
{
    private protected override async Task<ITransform3D> OnParse(IXmlReaderWrapper reader)
    {
        var position = IPosition3D.GetDefault();
        var rotation = IRotation3D.GetDefault();
        var quaternion = IQuaternion.GetDefault();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Position, XmlNodeType.Element, 1))
            {
                position = await DataParser.Parse<IPosition3D>(reader);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Quaternion, XmlNodeType.Element, 1))
            {
                quaternion = await DataParser.Parse<IQuaternion>(reader);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Rotation, XmlNodeType.Element, 1))
            {
                rotation = await DataParser.Parse<IRotation3D>(reader);
            }
        }
        
        return new Transform3D
        {
            Position = position,
            Quaternion= quaternion,
            Rotation = rotation
        };
    }
}
