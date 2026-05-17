namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a shield modification data model.
/// </summary>
public interface IShieldModificationData : IModificationData
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
