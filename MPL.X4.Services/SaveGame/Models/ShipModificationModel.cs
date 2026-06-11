namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a ship modification.
/// </summary>
internal class ShipModificationModel : ModificationModel, IShipModificationModel
{
    public required int CountermeasureCapacity { get; init; }

    public required int DeployableCapacity { get; init; }

    public required double Drag{ get; init; }

    public required double Mass{ get; init; }

    public required double MaximumHull{ get; init; }

    public required int MissileCapacity { get; init; }

    public required double RadarCloak{ get; init; }

    public required double RadarRange{ get; init; }

    public required double RegionDamage{ get; init; }

    public override ModificationType Type => ModificationType.Ship;

    public required double UnitCapacity{ get; init; }
}
