using MPL.X4.Services;

namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a shield modification.
/// </summary>
internal class ShieldModificationData : ModificationData, IShieldModificationData
{
    public override string ToString()
        => $"{Ware} - {DebugOutputHelper.GetPropertyValues<double>(this, x => x != 0)}";

    public required double Capacity { get; init; }

    public required double RechargeDelay { get; init; }

    public required double RechargeRate { get; init; }
}
