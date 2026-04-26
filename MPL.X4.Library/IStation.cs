namespace MPL.X4;

/// <summary>
/// An interface that defines a station.
/// </summary>
public interface IStation
{
    /// <summary>
    /// Gets the code of the station.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the identifier of the station.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets an indication of whether the station is known to the player.
    /// </summary>
    bool IsKnown { get; }

    /// <summary>
    /// Gets the name identifier of the station.
    /// </summary>
    ITextResourceReference? NameId { get; }

    /// <summary>
    /// Gets the owner of the station.
    /// </summary>
    string Owner { get; }

    /// <summary>
    /// Gets the position of the station.
    /// </summary>
    ISectorPosition Position { get; }

    /// <summary>
    /// Gets the trades on offer at the station.
    /// </summary>
    IEnumerable<ITrade> Trades { get; }
}
