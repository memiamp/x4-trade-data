namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a build storage.
/// </summary>
public interface IBuildStorageModel : IHasCode, IHasCargo, IHasIsKnown, IHasOwner, IHasShips, IHasTrades, IHasTransform, IIsWreckable, IModelWithId
{
}
