namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a ship modification.
/// </summary>
public interface IShipModificationModel : IModificationModel
{
    /// <summary>
    /// Gets the countermeasure capacity.
    /// </summary>
    int CountermeasureCapacity { get; }

    /// <summary>
    /// Gets the deployable capacity.
    /// </summary>
    int DeployableCapacity { get; }

    /// <summary>
    /// Gets the drag.
    /// </summary>
    double Drag { get; }

    /// <summary>
    /// Gets the mass.
    /// </summary>
    double Mass { get; }

    /// <summary>
    /// Gets the maximum hull.
    /// </summary>
    double MaximumHull { get; }

    /// <summary>
    /// Gets the missile capacity.
    /// </summary>
    int MissileCapacity { get; }

    /// <summary>
    /// Gets the radar cloak.
    /// </summary>
    double RadarCloak { get; }

    /// <summary>
    /// Gets the radar range.
    /// </summary>
    double RadarRange { get; }

    /// <summary>
    /// Gets the region damage.
    /// </summary>
    double RegionDamage { get; }

    /// <summary>
    /// Gets the unit capacity.
    /// </summary>
    double UnitCapacity { get; }
}
