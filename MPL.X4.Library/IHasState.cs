namespace MPL.X4;

/// <summary>
/// An interface that defines an element that supports a state attribute.
/// </summary>
public interface IHasState
{
    /// <summary>
    /// Gets the state of the element.
    /// </summary>
    string? State { get; }
}
