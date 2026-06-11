namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a modification.
/// </summary>
public interface IModificationModel : IModelWithId
{
    /// <summary>
    /// Gets the name of the modification.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the quality of the modification.
    /// </summary>
    ModificationQuality Quality { get; }

    /// <summary>
    /// Gets the type of the modification.
    /// </summary>
    ModificationType Type { get; }
}
