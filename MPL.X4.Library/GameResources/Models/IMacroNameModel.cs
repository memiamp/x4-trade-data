namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a macro name model.
/// </summary>
public interface IMacroNameModel : IModelWithId
{
    /// <summary>
    /// Gets the name of the macro.
    /// </summary>
    string Name { get; }
}
