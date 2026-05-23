namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines the data model of a ware.
/// </summary>
public interface IWareData
{
    /// <summary>
    /// Gets the component reference for this ware.
    /// </summary>
    /// <remarks>This is used to tie equipment macros to wares.</remarks>
    string? ComponentReference { get; }

    /// <summary>
    /// Gets the factory name resource of the ware.
    /// </summary>
    ITextResourceReference? FactoryNameResource { get; }

    /// <summary>
    /// Gets the group of the ware.
    /// </summary>
    string Group { get; }

    /// <summary>
    /// Gets the identifier of the ware.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the name resource of the ware.
    /// </summary>
    ITextResourceReference? NameResource { get; }

    /// <summary>
    /// Gets the transport mechanism of the ware.
    /// </summary>
    string Transport { get; }

    /// <summary>
    /// Gets the volume of the ware.
    /// </summary>
    int Volume { get; }
}
