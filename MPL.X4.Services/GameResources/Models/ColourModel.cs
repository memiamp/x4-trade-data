using System.Drawing;

namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a model of a colour.
/// </summary>
internal class ColourModel : IColourModel
{
    /// <summary>
    /// Gets the default colour model.
    /// </summary>
    /// <returns>An <see cref="IColourModel"/> that is the result.</returns>
    internal static IColourModel GetDefault()
        => new ColourModel
        {
            Colour = Color.White,
            Glow = 0,
            Id = string.Empty
        };

    public override string ToString()
        => $"{Id} - {Colour} {Glow}";

    public required Color Colour { get; init; }

    public required float Glow { get; init; }

    public required string Id { get; init; }
}
