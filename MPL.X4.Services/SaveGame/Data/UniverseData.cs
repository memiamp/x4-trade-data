namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a universe.
/// </summary>
internal class Universe : IUniverse
{
    public override string ToString()
        => $"Sectors {Sectors.Count()}";

    public required IEnumerable<ISector> Sectors { get; init; }
}
