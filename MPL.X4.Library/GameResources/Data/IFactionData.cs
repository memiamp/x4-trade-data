namespace MPL.X4.Data;

/// <summary>
/// An interface that defines the data model of a faction.
/// </summary>
public interface IFactionData
{
    /// <summary>
    /// Gets the acronym resource of the faction.
    /// </summary>
    ITextResourceReference? AcronymResource { get; }

    /// <summary>
    /// Gets the colour reference of the faction.
    /// </summary>
    string? ColourReference { get; }

    /// <summary>
    /// Gets the identifier of the faction.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the name resource of the faction.
    /// </summary>
    ITextResourceReference? NameResource { get; }
}
