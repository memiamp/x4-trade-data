namespace MPL.X4;

/// <summary>
/// An interface that defines a faction.
/// </summary>
public interface IFaction
{
    /// <summary>
    /// Gets the acronym of the faction.
    /// </summary>
    string? Acronym { get; }

    /// <summary>
    /// Gets the acronym resource of the faction.
    /// </summary>
    ITextResourceReference? AcronymResource { get; }

    /// <summary>
    /// Gets the colour reference of the faction.
    /// </summary>
    string? ColourRef { get; }

    /// <summary>
    /// Gets the identifier of the faction.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the name of the faction.
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// Gets the name resource of the faction.
    /// </summary>
    ITextResourceReference? NameResource { get; }
}
