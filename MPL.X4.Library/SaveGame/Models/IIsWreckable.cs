namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines an element that is wreckable.
/// </summary>
public interface IIsWreckable
{
    /// <summary>
    /// Gets an indication of whether the element is a wreck.
    /// </summary>
    bool IsWreck { get; }
}
