namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An enumeration that defines the type of a gate.
/// </summary>
public enum GateType
{
    /// <summary>
    /// The type is unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The gate is a jump gate.
    /// </summary>
    JumpGate,

    /// <summary>
    /// The gate is a super highway.
    /// </summary>
    Superhighway,

    /// <summary>
    /// The gate is a trans-orbital accelerator.
    /// </summary>
    TransorbitalAccelerator
}
