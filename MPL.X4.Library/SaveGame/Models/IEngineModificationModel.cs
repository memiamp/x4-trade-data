namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of an engine modification.
/// </summary>
public interface IEngineModificationModel : IModificationModel
{
    /// <summary>
    /// Gets the boost acceleration.
    /// </summary>
    double BoostAcceleration { get; }

    /// <summary>
    /// Gets the boost duration.
    /// </summary>
    double BoostDuration { get; }

    /// <summary>
    /// Gets the boost thrust.
    /// </summary>
    double BoostThrust { get; }

    /// <summary>
    /// Gets the forward thrust.
    /// </summary>
    double ForwardThrust { get; }

    /// <summary>
    /// Gets the rotation thrust.
    /// </summary>
    double RotationThrust { get; }

    /// <summary>
    /// Gets the strafe acceleration.
    /// </summary>
    double StrafeAcceleration { get; }

    /// <summary>
    /// Gets the strafe thrust.
    /// </summary>
    double StrafeThrust { get; }

    /// <summary>
    /// Gets the travel attack time.
    /// </summary>
    double TravelAttackTime { get; }

    /// <summary>
    /// Gets the travel charge time.
    /// </summary>
    double TravelChargeTime { get; }

    /// <summary>
    /// Gets the travel start thrust.
    /// </summary>
    double TravelStartThrust { get; }

    /// <summary>
    /// Gets the travel thrust.
    /// </summary>
    double TravelThrust { get; }
}
