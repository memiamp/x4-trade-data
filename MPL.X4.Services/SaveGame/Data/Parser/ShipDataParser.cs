using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

using ShipElements = (
                      CargoData CargoData,
                      IEnumerable<string> Modifications,
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
            Owner = owner,
            Transform = transform
        };
    }

    private async Task<ShipElements> ParseElements(IXmlReaderWrapper reader)
    {
        var cargoItems = new List<ICargoItemData>();
        var modifications = new List<string>();
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

                var data = await ParseModifications(subtree);

                modifications.AddRange(data);
            }
            /*
            <shields>
                <group>
                    <modification ware="mod_shield_rechargerate_01_mk3" rechargedelay="0.339787" rechargerate="1.68667"/>
                </group>
            </shields>
            */
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Modification, XmlNodeType.Element, 3))
            {
                modifications.Add("Shield");
            }
            /*
            <connections>
                <connection connection="con_weapon_03">
                    <component class="weapon" macro="weapon_spl_m_shotgun_01_mk2_macro" connection="weaponcon_01" lastshottime="355048.508" id="[0x7ac51]">
                        <modification ware="mod_weapon_damage_02_mk3" damage="1.18599" cooling="1.30893" reload="1.10112" sticktime="1.10831"/>
                    </component>
                </connection>
            </connections>
            */
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Modification, XmlNodeType.Element, 4))
            {
                modifications.Add("Weapon");
            }
        }

        return (new CargoData { Items = cargoItems }, modifications, transform);
    }

    private static async Task<IEnumerable<string>> ParseModifications(IXmlReaderWrapper reader)
    {
        /*
        <modification>
            <engine ware="mod_engine_forwardthrust_01_mk3" forwardthrust="1.19757" rotationthrust="1.20424" boostthrust="1.37198" travelthrust="1.2086" strafeacc="1.20435"/>
            <paint ware="paintmod_0188"/>
            <ship ware="mod_ship_mass_01_mk3" mass="0.796761" drag="0.834835"/>
        </modification>
        */
        var returnValue = new List<string>();

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Engine, XmlNodeType.Element, 1))
            {
                returnValue.Add("Engine");
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Paint, XmlNodeType.Element, 1))
            {
                returnValue.Add("Paint");
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Ship, XmlNodeType.Element, 1))
            {
                returnValue.Add("Ship");
            }
        }

        return returnValue;
    }
}
