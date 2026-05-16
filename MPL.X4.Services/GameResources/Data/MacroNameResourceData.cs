namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements a data model of a macro name resource.
/// </summary>
internal class MacroNameResourceData : IMacroNameResourceData
{
    public override string ToString()
        => $"{Id} - {NameResource}";

    public required string Id { get; init; }

    public required ITextResourceReference NameResource { get; init; }
}
