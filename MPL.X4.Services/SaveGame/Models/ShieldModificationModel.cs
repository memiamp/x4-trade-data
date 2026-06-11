namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a shield modification.
/// </summary>
internal class ShieldModificationModel : ModificationModel, IShieldModificationModel
{
    public required double Capacity{ get; init; }

    public required double RechargeDelay{ get; init; }

    public required double RechargeRate{ get; init; }

    public override ModificationType Type => ModificationType.Shield;
}
