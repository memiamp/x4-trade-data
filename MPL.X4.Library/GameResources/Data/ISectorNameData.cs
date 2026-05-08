namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines a sector name data model.
/// </summary>
public interface ISectorNameData
{
    /// <summary>
    /// Gets the sector macro.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the name resource of the sector.
    /// </summary>
    ITextResourceReference NameResource { get; }
}
