namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements an offset.
/// </summary>
internal class Colour : IColour
{
    public override string ToString()
        => $"{Id} - {Red},{Green},{Blue},{Alpha}";

    public required int Alpha { get; init; }

    public required int Blue { get; init; }

    public required float Glow { get; init; }

    public required int Green { get; init; }

    public required string Id { get; init; }

    public required int Red { get; init; }
}
