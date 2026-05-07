namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines a zone offset data model.
/// </summary>
public interface IZoneOffsetData : IOffsetData
{
    /// <summary>
    /// Gets the macro name of the zone offset.
    /// </summary>
    string MacroName { get; }
}
