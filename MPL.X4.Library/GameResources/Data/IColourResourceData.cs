namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines the data model of colour resources.
/// </summary>
public interface IColourResourceData
{
    /// <summary>
    /// Gets the colours.
    /// </summary>
    IColourDataDictionary Colours { get; }

    /// <summary>
    /// Gets the colour mappings.
    /// </summary>
    IMappingDataDictionary Mappings { get; }
}
