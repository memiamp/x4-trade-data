namespace MPL.X4;

/// <summary>
/// An interface that defines an element that supports a transform.
/// </summary>
public interface IHasTransform
{
    /// <summary>
    /// Gets the transform of the element.
    /// </summary>
    ITransform3D Transform { get; }
}
