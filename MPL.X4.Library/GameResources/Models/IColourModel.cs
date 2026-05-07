using System.Drawing;

namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a colour model.
/// </summary>
public interface IColourModel
{
    /// <summary>
    /// Gets the colour.
    /// </summary>
    Color Colour { get; }

    /// <summary>
    /// Gets the colour glow.
    /// </summary>
    float Glow { get; }

    /// <summary>
    /// Gets the identifier of the colour.
    /// </summary>
    string Id { get; }
}
