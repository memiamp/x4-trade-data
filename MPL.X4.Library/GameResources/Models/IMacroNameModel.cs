namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a sector name model.
/// </summary>
public interface ISectorNameModel : IModelWithId
{
    /// <summary>
    /// Gets the name of the sector.
    /// </summary>
    string Name { get; }
}
