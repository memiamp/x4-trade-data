namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a highway.
/// </summary>
public interface IHighwayData : IHasTransform
{
    /// <summary>
    /// Gets the code of the highway.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the identifier of the highway.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets an indication of whether the highway is known to the player.
    /// </summary>
    bool IsKnown { get; }

    /// <summary>
    /// Gets the highway macro.
    /// </summary>
    string Macro { get; }

    /// <summary>
    /// Gets the ships in the zone.
    /// </summary>
    IEnumerable<IShipData> Ships { get; }
}
