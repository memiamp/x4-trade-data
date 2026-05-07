namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a gate.
/// </summary>
public interface IGateData
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
    IPosition3D Position { get; }
}
