namespace MPL.X4.SaveGame.Models.Services;

/// <summary>
/// A class that implements extensions methods to an <see cref="IShipModel"/>.
/// </summary>
public static class ShipModelExtensions
{
    /// <summary>
    /// Flattens the specified <paramref name="source"/> to return ships from all levels of the hierarchy.
    /// </summary>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of type <see cref="IShipModel"/> that is the source to be flattened.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="IShipModel"/> that is the result.</returns>
    public static IEnumerable<IShipModel> Flatten(this IEnumerable<IShipModel> source)
    {
        foreach (var ship in source)
        {
            yield return ship;
            foreach (var child in ship.Ships.Flatten())
            {
                yield return child;
            }
        }
    }
}
