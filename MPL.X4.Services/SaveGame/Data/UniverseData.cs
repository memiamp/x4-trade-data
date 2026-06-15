namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a universe.
/// </summary>
internal class UniverseData : IUniverseData
{
    public required IGalaxyData Galaxy { get; init; }
}
