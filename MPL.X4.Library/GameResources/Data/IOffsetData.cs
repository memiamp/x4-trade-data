namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines an offset data model.
/// </summary>
public interface IOffsetData
{
    /// <summary>
    /// Gets the position.
    /// </summary>
    IPosition3D Position { get; }

    /// <summary>
    /// Gets the rotation.
    /// </summary>
    IRotation3D Rotation { get; }
}
