namespace MPL.X4;

/// <summary>
/// An interface that defines a zone offset.
/// </summary>
public interface IZoneOffset
{
    /// <summary>
    /// Gets the macro name of the zone offset.
    /// </summary>
    string MacroName { get; }

    /// <summary>
    /// Gets the offset position.
    /// </summary>
    ISectorPosition OffsetPosition { get; }

    /// <summary>
    /// Gets the offset rotation.
    /// </summary>
    ISectorRotation OffsetRotation { get; }
}
