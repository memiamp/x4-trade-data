namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines the base model of a sector element with cargo.
/// </summary>
internal class SectorElementWithCargoModelBase  : SectorElementModelBase, IHasCargo
{
    public required ICargoItemModelList Cargo { get; init; }
}
