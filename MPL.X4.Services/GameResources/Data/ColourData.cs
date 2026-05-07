namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements a data model of a colour.
/// </summary>
internal class ColourData : IColourData
{
    /// <summary>
    /// Gets the default colour.
    /// </summary>
    /// <returns>An <see cref="IColourData"/> that is the result.</returns>
    internal static IColourData GetDefault()
        => new ColourData
        {
            Alpha = 1,
            Blue = 1,
            Glow = 0,
            Green = 1,
            Id = string.Empty,
            Red = 1
        };

    public override string ToString()
        => $"{Id} - {Red},{Green},{Blue},{Alpha}";

    public required int Alpha { get; init; }

    public required int Blue { get; init; }

    public required float Glow { get; init; }

    public required int Green { get; init; }

    public required string Id { get; init; }

    public required int Red { get; init; }
}
