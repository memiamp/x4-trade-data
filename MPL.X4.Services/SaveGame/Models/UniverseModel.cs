namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a universe.
/// </summary>
internal class UniverseModel : IUniverseModel
{
    public override string ToString()
        => $"Sectors {Sectors.Count}";

    public required ISectorModelList Sectors { get; init; }
}
