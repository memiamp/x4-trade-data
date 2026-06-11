namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines an element that has a identifier code.
/// </summary>
public interface IHasCode
{
    /// <summary>
    /// Gets the code of the element.
    /// </summary>
    string Code { get; }
}
