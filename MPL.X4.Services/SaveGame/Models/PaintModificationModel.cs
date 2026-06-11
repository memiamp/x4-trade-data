namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a paint modification.
/// </summary>
internal class PaintModificationModel : ModificationModel, IPaintModificationModel
{
    public required bool IsGenerated { get; init; }

    public override ModificationType Type => ModificationType.Paint;
}
