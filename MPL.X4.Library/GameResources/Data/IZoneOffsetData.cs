namespace MPL.X4;

/// <summary>
/// An interface that defines a zone offset.
/// </summary>
public interface IZoneOffset : IOffset
{
    /// <summary>
    /// Gets the macro name of the zone offset.
    /// </summary>
    string MacroName { get; }
}
