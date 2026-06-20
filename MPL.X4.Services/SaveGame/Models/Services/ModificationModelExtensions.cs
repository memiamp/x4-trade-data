namespace MPL.X4.SaveGame.Models.Services;

/// <summary>
/// A class that implements extensions methods to an <see cref="IModificationModel"/>.
/// </summary>
public static class ModificationModelExtensions
{
    /// <summary>
    /// Increments the appropriate modification counters depending upon the quality of the modifications in the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of type <see cref="IModificationModel"/> that are the source models.</param>
    /// <param name="basic">A <see langword="ref"/> <see cref="int"/> that will be incremented where the quality is <see cref="ModificationQuality.Basic"/>.</param>
    /// <param name="enhanced">A <see langword="ref"/> <see cref="int"/> that will be incremented where the quality is <see cref="ModificationQuality.Enhanced"/>.</param>
    /// <param name="exceptional">A <see langword="ref"/> <see cref="int"/> that will be incremented where the quality is <see cref="ModificationQuality.Exceptional"/>.</param>
    public static void GetModificationQuality(this IEnumerable<IModificationModel> source, ref int basic, ref int enhanced, ref int exceptional)
    {
        foreach (var item in source)
        {
            GetModificationQuality(item, ref basic, ref enhanced, ref exceptional);
        }
    }

    /// <summary>
    /// Increments the appropriate modification counter depending upon the quality of the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="IModificationModel"/> that is the source model.</param>
    /// <param name="basic">A <see langword="ref"/> <see cref="int"/> that will be incremented where the quality is <see cref="ModificationQuality.Basic"/>.</param>
    /// <param name="enhanced">A <see langword="ref"/> <see cref="int"/> that will be incremented where the quality is <see cref="ModificationQuality.Enhanced"/>.</param>
    /// <param name="exceptional">A <see langword="ref"/> <see cref="int"/> that will be incremented where the quality is <see cref="ModificationQuality.Exceptional"/>.</param>
    public static void GetModificationQuality(this IModificationModel? source, ref int basic, ref int enhanced, ref int exceptional)
    {
        if (source?.Quality == ModificationQuality.Basic)
            basic++;
        else if (source?.Quality == ModificationQuality.Enhanced)
            enhanced++;
        else if (source?.Quality == ModificationQuality.Exceptional)
            exceptional++;
    }
}
