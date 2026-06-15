namespace MPL.X4.SaveGame.Models.Services;

/// <summary>
/// A class that implements extensions methods to an <see cref="IUniverseModel"/>.
/// </summary>
public static class UniverseModelExtensions
{
    /// <summary>
    /// Gets an enumeration containing all ships in the universe from all sources.
    /// </summary>
    /// <param name="source">An <see cref="IUniverseModel"/> that is the universe.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="IShipModel"/> that is the result.</returns>
    public static IEnumerable<IShipModel> GetAllShips(this IUniverseModel source)
        => source
                 .HighwayShips
                 .Flatten()
                 .Concat(
                         source
                               .Sectors
                               .SelectMany(x => x.GetAllShips()));
}
