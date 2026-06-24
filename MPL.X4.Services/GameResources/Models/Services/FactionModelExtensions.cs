namespace MPL.X4.GameResources.Models.Services;

/// <summary>
/// A class that implements extensions methods to a <see cref="IFactionModel"/>.
/// </summary>
public static class FactionModelExtensions
{
    /// <summary>
    /// Gets an indication of whether the specified <paramref name="source"/> is player-owned.
    /// </summary>
    /// <param name="source">A nullable <see cref="IFactionModel"/> that is the source to evaluate.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    public static bool IsPlayerOwned(this IFactionModel? source)
       => source?.Name == Constants.Factions.PlayerFactionName;
}
