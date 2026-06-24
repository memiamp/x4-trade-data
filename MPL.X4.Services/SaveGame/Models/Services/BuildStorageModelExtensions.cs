using MPL.X4.GameResources.Models.Services;

namespace MPL.X4.SaveGame.Models.Services;

/// <summary>
/// A class that implements extensions methods to an <see cref="IBuildStorageModel"/>.
/// </summary>
public static class BuildStorageModelExtensions
{
    /// <summary>
    /// Gets an indication of whether the specified <paramref name="source"/> is player-owned.
    /// </summary>
    /// <param name="source">A nullable <see cref="IBuildStorageModel"/> that is the source to evaluate.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    public static bool IsPlayerOwned(this IBuildStorageModel source)
        => source.Owner.IsPlayerOwned();
}
