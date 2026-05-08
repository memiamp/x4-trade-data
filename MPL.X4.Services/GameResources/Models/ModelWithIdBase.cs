namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements the base functionality of a model that supports an identifier.
/// </summary>
internal abstract class ModelWithIdBase : IModelWithId
{
    public required string Id { get; init; }
}
