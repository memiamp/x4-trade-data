namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a shield modification.
/// </summary>
internal class ShieldModificationData : ModificationData, IShieldModificationData
{
    public override string ToString()
        => $"{Ware} - Capacity : {Capacity} Recharge Delay: {RechargeDelay} Recharge Rate: {RechargeRate}";

    public required double Capacity { get; init; }

    public required double RechargeDelay { get; init; }

    public required double RechargeRate { get; init; }
}
