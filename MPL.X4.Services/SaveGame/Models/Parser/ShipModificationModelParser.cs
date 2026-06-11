using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IShipModificationModel"/> from an <see cref="IShipModificationData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class ShipModificationModelParser(
                                           ILogger<ShipModificationModelParser> logger,
                                           ISaveGameModelParsingScope parsingScope)
    : ModificationModelParserBase<IShipModificationData, IShipModificationModel>(logger, parsingScope)
{
    private protected override IShipModificationModel OnParse(IShipModificationData source)
    {
        ParseNameAndQuality(source.Ware, out var name, out var quality);

        return new ShipModificationModel
        {
            CountermeasureCapacity = source.CountermeasureCapacity,
            DeployableCapacity = source.DeployableCapacity,
            Drag = source.Drag,
            Id = source.Ware,
            Mass = source.Mass,
            MaximumHull = source.MaximumHull,
            MissileCapacity = source.MissileCapacity,
            Name = name,
            Quality = quality,
            RadarCloak = source.RadarCloak,
            RadarRange = source.RadarRange,
            RegionDamage = source.RegionDamage,
            UnitCapacity = source.UnitCapacity
        };
    }
}
