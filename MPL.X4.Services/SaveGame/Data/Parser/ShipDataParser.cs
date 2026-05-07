using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="IShip"/>.
/// </summary>
/// <param name="cargoParser">An <see cref="IDataParser{ICargo}"/> that is the cargo parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{ISectorPosition}"/> that is the position parser to use.</param>
internal class ShipParser(
                          IDataParser<ICargo> cargoParser,
                          ILogger<ShipParser> logger,
                          IDataParser<ISectorPosition> positionParser)
    : PositionalDataParserBase<IShip>(logger, positionParser)
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

    private protected override async Task<IShip> OnParse(IXmlReaderWrapper reader)
    //private protected override async Task<IShip> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        var cargoItems = new List<ICargoItem>();
        var modifications = new List<string>();

        SectorPosition position = new();
        //SectorPosition position = new(positionOffset);
        Ship? returnValue;

        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, out string? shipClass) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Owner, out string? owner) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out string? macro))
        {
            var isKnown = GetIsKnownToPlayer(reader);

            returnValue = new Ship
            {
                Cargo = new Cargo
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
