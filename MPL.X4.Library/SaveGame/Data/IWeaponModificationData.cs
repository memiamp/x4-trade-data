namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a weapon modification data model.
/// </summary>
public interface IWeaponModificationData : IModificationData
{
    /// <summary>
    /// Gets the charge time.
    /// </summary>
    double ChargeTime { get; }

    /// <summary>
    /// Gets the cooling rate.
    /// </summary>
    double Cooling { get; }

    /// <summary>
    /// Gets the damage rate.
    /// </summary>
    double Damage { get; }

    /// <summary>
    /// Gets the mining rate.
    /// </summary>
    double Lifetime { get; }

    /// <summary>
    /// Gets the mining rate.
    /// </summary>
    double Mining { get; }

    /// <summary>
    /// Gets the reload rate.
    /// </summary>
    double Reload { get; }

    /// <summary>
    /// Gets the rotation speed.
    /// </summary>
    double RotationSpeed { get; }

    /// <Reload>
    /// Gets the stick time.
    /// </summary>
    double StickTime { get; }

    /// <summary>
    /// Gets the surface damage element.
    /// </summary>
    double SurfaceElement { get; }
}
