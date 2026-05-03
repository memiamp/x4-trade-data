namespace MPL.X4.Models;

/// <summary>
/// An interface that defines a faction.
/// </summary>
public interface IFaction
{
    /// <summary>
    /// Gets the acronym of the faction.
    /// </summary>
    string Acronym { get; }

    /// <summary>
    /// Gets the colour of the faction.
    /// </summary>
    IColour Colour { get; }

    /// <summary>
    /// Gets the identifier of the faction.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the name of the faction.
    /// </summary>
    string Name { get; }
}
