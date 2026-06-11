using MPL.X4.GameResources.Models;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines the base sector model.
/// </summary>
internal class SectorModelBase : ModelWithIdBase, IHasCode, IHasIsKnown, IHasOwner, IHasShips, IHasTransform
{
    public required string Code { get; init; }

    public required bool IsKnown { get; init; }

    public required IFactionModel Owner { get; init; }

    public required IShipModelList Ships { get; init; }

    public required ITransform3D Transform { get; init; }
}
