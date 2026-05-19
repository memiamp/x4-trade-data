namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a faction model.
/// </summary>
public interface IFactionModel : IModelWithId
{
    /// <summary>
    /// Gets the acronym of the faction.
    /// </summary>
    string Acronym { get; }

    /// <summary>
    /// Gets the colour of the faction.
    /// </summary>
    IColourModel Colour { get; }

    /// <summary>
    /// Gets an indication of whether this is an ownerless faction.
    /// </summary>
    bool IsOwnerless => Id == Constants.XmlDataFile.AttributeValue.Owner.Ownerless;

    /// <summary>
    /// Gets the name of the faction.
    /// </summary>
    string Name { get; }
}
