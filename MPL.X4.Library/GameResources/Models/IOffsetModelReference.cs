namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a refernece of offset models.
/// </summary>
public interface IOffsetModelReference
{
    /// <summary>
    /// Gets other offsets that are not otherwise provided.
    /// </summary>
    IDictionary<string, IOffsetModelList> Other { get; }

    /// <summary>
    /// Gets a list of sector offsets.
    /// </summary>
    IOffsetModelList Sectors { get; }

    /// <summary>
    /// Gets a list of zone offsets.
    /// </summary>
    IOffsetModelList Zones { get; }
}
