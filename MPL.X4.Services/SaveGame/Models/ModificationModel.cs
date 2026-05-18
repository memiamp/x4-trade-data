namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a modification.
/// </summary>
internal class ModificationModel : ModelWithIdBase, IModificationModel
{
    public override string ToString()
        => $"{Name} - {Summary} - {Id}";

    public required string Name { get; init; }

    public required string Summary { get; init; }

    public required ModificationType Type { get; init; }
}
