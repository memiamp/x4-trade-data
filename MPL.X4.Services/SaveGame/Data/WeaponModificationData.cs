using MPL.X4.Services;

namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a weapon modification.
/// </summary>
internal class WeaponModificationData : ModificationData, IWeaponModificationData
{
    public override string ToString()
        => $"{Ware} - {DebugOutputHelper.GetPropertyValues<double>(this, x => x != 0)}";

    public required double ChargeTime { get; init; }

    public required double Cooling { get; init; }

    public required double Damage { get; init; }

    public required double Lifetime { get; init; }

    public required double Mining { get; init; }

    public required double Reload { get; init; }

    public required double RotationSpeed { get; init; }

    public required double StickTime { get; init; }

    public required double SurfaceElement { get; init; }
}
