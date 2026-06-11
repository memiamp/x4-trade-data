namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An enumeration that defines the quality of a modification.
/// </summary>
public enum ModificationQuality
{
    /// <summary>
    /// The modification quality is unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The type is a basic modification.
    /// </summary>
    Basic,

    /// <summary>
    /// The type is an enhanced modification.
    /// </summary>
    Enhanced,

    /// <summary>
    /// The type is an exceptional modification.
    /// </summary>
    Exceptional
}
