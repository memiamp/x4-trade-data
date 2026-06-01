namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a production.
/// </summary>
public interface IProductionModel : IModelWithId
{
    /// <summary>
    /// Gets the number of modules producing this ware.
    /// </summary>
    int ModuleCount { get; }

    /// <summary>
    /// Gets the name of the produced ware.
    /// </summary>
    string Name { get; }
}
