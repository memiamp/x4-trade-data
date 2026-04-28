namespace MPL.X4;

/// <summary>
/// An interface that defines a gate.
/// </summary>
public interface IGate
{
    /// <summary>
    /// Gets the code of the gate.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the identifier of the gate.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets an indication of whether the gate is known to the player.
    /// </summary>
    bool IsKnown { get; }

    /// <summary>
    /// Gets the position of the gate.
    /// </summary>
    ISectorPosition Position { get; }
}
