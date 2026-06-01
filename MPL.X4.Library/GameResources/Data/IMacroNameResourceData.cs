namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines a macro name resource data model.
/// </summary>
public interface IMacroNameResourceData
{
    /// <summary>
    /// Gets the macro.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the name resource of the macro.
    /// </summary>
    ITextResourceReference NameResource { get; }
}
