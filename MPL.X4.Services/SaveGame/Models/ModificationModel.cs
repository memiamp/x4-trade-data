namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a modification.
/// </summary>
internal abstract class ModificationModel : ModelWithIdBase, IModificationModel
{
    public override string ToString()
        => $"{Name} - {Id}";

    public required string Name { get; init; }

    public required ModificationQuality Quality { get; init; }
    
    public abstract ModificationType Type { get; }
}
