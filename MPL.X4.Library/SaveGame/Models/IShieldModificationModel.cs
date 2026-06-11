namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a shield modification.
/// </summary>
public interface IShieldModificationModel : IModificationModel
{
    /// <summary>
    /// Gets the capacity.
    /// </summary>
    double Capacity { get; }

    /// <summary>
    /// Gets the recharge delay.
    /// </summary>
    double RechargeDelay { get; }

    /// <summary>
    /// Gets the recharge rate.
    /// </summary>
    double RechargeRate { get; }
}
