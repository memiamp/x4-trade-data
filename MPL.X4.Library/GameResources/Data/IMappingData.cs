namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines the data model of a mapping.
/// </summary>
public interface IMappingData
{
    /// <summary>
    /// Gets the identifier to be mapped.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the mapping reference.
    /// </summary>
    string Reference  { get; }
}
