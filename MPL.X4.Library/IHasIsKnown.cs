namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines an element that supports an Is Known attribute.
/// </summary>
public interface IHasIsKnown
{
    /// <summary>
    /// Gets an indication of whether the element is known to the player.
    /// </summary>
    bool IsKnown { get; }
}
