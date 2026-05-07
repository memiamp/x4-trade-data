namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a universe.
/// </summary>
internal class UniverseData : IUniverseData
{
    public override string ToString()
        => $"Sectors {Sectors.Count()}";

    public required IEnumerable<ISectorData> Sectors { get; init; }
}
