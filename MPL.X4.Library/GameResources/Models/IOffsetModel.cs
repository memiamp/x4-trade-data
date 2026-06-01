namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines an offset model.
/// </summary>
public interface IOffsetModel : IModelWithId
{
    /// <summary>
    /// Gets the name of the offset.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the offset.
    /// </summary>
    ITransform3D Offset { get; }
}
