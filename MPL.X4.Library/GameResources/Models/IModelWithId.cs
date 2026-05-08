namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a model that supports an identifier.
/// </summary>
public interface IModelWithId
{
    /// <summary>
    /// Gets the identifier of the model.
    /// </summary>
    string Id { get; }
}
