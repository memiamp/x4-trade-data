using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.DataParser;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.SaveGame.Data.Parser;

/// <summary>
/// A class that implements a data parser for a <see cref="IShipData"/>.
/// </summary>
/// <param name="cargoParser">An <see cref="IDataParser{ICargoData}"/> that is the cargo parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{IPosition3D}"/> that is the position parser to use.</param>
internal class ShipDataParser(
                              IDataParser<ICargoData> cargoParser,
                              ILogger<ShipDataParser> logger,
                              IDataParser<IPosition3D> positionParser)
    : PositionalDataParserBase<IShipData>(logger, positionParser)
{
    private async Task<IEnumerable<string>> ProcessModifications(IXmlReaderWrapper reader)
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

    private protected override async Task<IShipData> OnParse(IXmlReaderWrapper reader)
    //private protected override async Task<IShip> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        var cargoItems = new List<ICargoItemData>();
        var modifications = new List<string>();

        Position3D position = new();
        //SectorPosition position = new(positionOffset);
        ShipData? returnValue;

        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, out string? shipClass) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Owner, out string? owner) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out string? macro))
        {
            var isKnown = GetIsKnownToPlayer(reader);

            returnValue = new ShipData
            {
                Cargo = new CargoData
                {
                    Items = cargoItems
                },
                Class = shipClass,
                Code = code,
                Id = id,
                IsKnown = isKnown,
                Macro = macro,
                Modifications = modifications,
                Owner = owner,
                Position = position
            };
        }
        else
        {
            logger.LogWarning("Could not load ship");
            throw new ArgumentException("Could not load ship", nameof(reader));
        }

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Offset, XmlNodeType.Element, 1))
            {
                await UpdatePositionFromOffset(reader, position);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Cargo, XmlNodeType.Element))
            {
                using var cargoSubtree = await reader.ReadSubtree();

                var cargoes = await cargoParser.Parse(cargoSubtree);

                cargoItems.AddRange(cargoes.Items);
            }
            else if (reader.CheckNodeMatches(Constants.XmlDataFile.ElementName.Modification, XmlNodeType.Element, 1))
            {
                using var modificationsSubTree = await reader.ReadSubtree();

                var shipModifications = await ProcessModifications(modificationsSubTree);

                modifications.AddRange(shipModifications);
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

        return returnValue;
    }
}
