namespace MPL.X4.GameResources.Models;

/// <summary>
/// A class that implements a macro name model.
/// </summary>
internal class MacroNameModel : ModelWithIdBase, IMacroNameModel
{
    public override string ToString()
        => $"{Name} - {Id}";

    public required string Name { get; init; }
}
