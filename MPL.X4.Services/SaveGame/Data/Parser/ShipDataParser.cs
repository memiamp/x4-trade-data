using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

using ShipElements = (
                      CargoData CargoData,
                      IEnumerable<IModificationData> Modifications,
                      ITransform3D Transform);

/// <summary>
/// A class that implements a data parser for a <see cref="IShipData"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ShipDataParser(
                              IDataParser dataParser,
                              ILogger<ShipDataParser> logger)
    : DataParserBase<IShipData>(dataParser, logger)
{
    private protected override async Task<IShipData> OnParse(IXmlReaderWrapper reader)
    {
        if (!reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Class, out string? shipClass) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Owner, out string? owner) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Code, out string? code) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Macro, out string? macro) ||
            !reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.ShipId, out string? id))
        {
            logger.LogWarning("Could not load ship");
            throw new ArgumentException("Could not load ship", nameof(reader));
        }

        var isKnown = GetIsKnownToPlayer(reader);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.Name, out string? name);

        var (cargo, modifications, transform) = await ParseElements(reader);

        return new ShipData
        {
            Cargo = cargo,
            Class = shipClass,
            Code = code,
            Id = id,
            IsKnown = isKnown,
            Macro = macro,
            Modifications = modifications,
            Name = name,
            Owner = owner,
            Transform = transform
        };
    }

    private async Task<ShipElements> ParseElements(IXmlReaderWrapper reader)
    {
        var cargoItems = new List<IWareItemData>();
        var modifications = new List<IModificationData>();
        ITransform3D transform = ITransform3D.GetDefault();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                transform = await DataParser.Parse<ITransform3D>(subtree);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Cargo, XmlNodeType.Element))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<ICargoData>(subtree);

                cargoItems.AddRange(data.Items);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Modification, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<IEnumerable<IModificationData>>(subtree);

                modifications.AddRange(data);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Shields, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<IEnumerable<IModificationData>>(subtree);

                modifications.AddRange(data);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Connections, XmlNodeType.Element, 1))
            {
                using var subtree = await reader.ReadSubtree();

                var data = await DataParser.Parse<IEnumerable<IModificationData>>(subtree);

                modifications.AddRange(data);
            }
        }

        return (new CargoData { Items = cargoItems }, modifications, transform);
    }
}
