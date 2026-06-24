using MPL.X4.GameResources.Models.Services;

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

    /// <summary>
    /// Gets the count of modification qualities for the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="IShipModel"/> that is the source to evaluate.</param>
    /// <returns>A <see cref="Tuple{T1, T2, T3}"/> of <see cref="int"/> indicating the number of basic, enhanced, and exceptional, qualities.</returns>
    public static (int Basic, int Enhanced, int Exceptional) GetModificationQualityCount(this IShipModel source)
    {
        var basic = 0;
        var enhanced = 0;
        var exceptional = 0;

        source.EngineModification.GetModificationQuality(ref basic, ref enhanced, ref exceptional);
        source.PaintModification.GetModificationQuality(ref basic, ref enhanced, ref exceptional);
        source.ShieldModification.GetModificationQuality(ref basic, ref enhanced, ref exceptional);
        source.ShipModification.GetModificationQuality(ref basic, ref enhanced, ref exceptional);
        source.WeaponModifications.GetModificationQuality(ref basic, ref enhanced, ref exceptional);

        return (basic, enhanced, exceptional);
    }

    /// <summary>
    /// Gets all modifications for the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="IShipModel"/> that is the source to evaluate.</param>
    /// <param name="includePaintModification">A <see cref="bool"/> indicating whether to include paint modifications in the evaluation.  Default is <see langword="false"/>.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of type <see cref="IModificationModel"/> that is the result.</returns>
    public static IEnumerable<IModificationModel> GetModifications(this IShipModel source, bool includePaintModification = false)
    {
        List<IModificationModel> returnValue = [];

        if (source.EngineModification is not null)
        {
            returnValue.Add(source.EngineModification);
        }

        if (source.ShieldModification is not null)
        {
            returnValue.Add(source.ShieldModification);
        }

        if (source.ShipModification is not null)
        {
            returnValue.Add(source.ShipModification);
        }

        if (includePaintModification &&
            source.PaintModification is not null)
        {
            returnValue.Add(source.PaintModification);
        }

        foreach (var item in source.WeaponModifications)
        {
            returnValue.Add(item);
        }

        return returnValue;
    }

    /// <summary>
    /// Gets an indication of whether the specified <paramref name="source"/> has any ship modifications.
    /// </summary>
    /// <param name="source">An <see cref="IShipModel"/> that is the source to evaluate.</param>
    /// <param name="includePaintModification">A <see cref="bool"/> indicating whether to include paint modifications in the evaluation.  Default is <see langword="false"/>.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    public static bool HasModifications(this IShipModel source, bool includePaintModification = false)
        => source.EngineModification is not null ||
           source.ShieldModification is not null ||
           source.ShipModification is not null ||
           source.WeaponModifications.Any() ||
           includePaintModification &&
           source.PaintModification is not null;

    /// <summary>
    /// Gets an indication of whether the specified <paramref name="source"/> is player-owned.
    /// </summary>
    /// <param name="source">A nullable <see cref="IShipModel"/> that is the source to evaluate.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    public static bool IsPlayerOwned(this IShipModel source)
        => source.Owner.IsPlayerOwned();
}
