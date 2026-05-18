namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An enumeration that defines the type of a modification.
/// </summary>
public enum ModificationType
{
    /// <summary>
    /// The modification type is unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The type is an engine modification.
    /// </summary>
    Engine,

    /// <summary>
    /// The type is a paint modification.
    /// </summary>
    Paint,

    /// <summary>
    /// The type is a shield modification.
    /// </summary>
    Shield,

    /// <summary>
    /// The type is a ship modification.
    /// </summary>
    Ship,

    /// <summary>
    /// The type is a weapon modification.
    /// </summary>
    Weapon
}
