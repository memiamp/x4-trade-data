namespace MPL.X4.Services.Models;

/// <summary>
/// A class that implements a cargo.
/// </summary>
internal class Cargo : ICargo
{
    public override string ToString()
        => $"Cargo count: {Items.Count()}";

    public required IEnumerable<ICargoItem> Items { get; init; }
}
