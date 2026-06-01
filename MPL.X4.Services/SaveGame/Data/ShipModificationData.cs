using MPL.X4.Services;

namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a ship modification.
/// </summary>
internal class ShipModificationData : ModificationData, IShipModificationData
{
    public override string ToString()
        => $"{Ware} - {DebugOutputHelper.GetPropertyValues<double>(this, x => x != 0)}";

    public required int CountermeasureCapacity { get; init; }

    public required int DeployableCapacity { get; init; }

    public required double Drag { get; init; }

    public required double Mass { get; init; }

    public required double MaximumHull { get; init; }

    public required int MissileCapacity { get; init; }

    public required double RadarCloak { get; init; }

    public required double RadarRange { get; init; }

    public required double RegionDamage { get; init; }

    public required double UnitCapacity { get; init; }
}
