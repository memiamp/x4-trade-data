using MPL.X4.SaveGame.Models;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements save game models.
/// </summary>
internal class SaveGameModels : ISaveGameModels
{
    public required IUniverseModel Universe { get; init; }
}
