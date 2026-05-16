namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a sector.
/// </summary>
public interface ISectorModel : IHasIsKnown, IHasTransform, IModelWithId
{
    /// <summary>
    /// Gets the code of the sector.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the gates in the sector.
    /// </summary>
    IGateModelList Gates { get; }

    /// <summary>
    /// Gets the lockboxes in the sector.
    /// </summary>
    ILockboxModelList Lockboxes { get; }

    /// <summary>
    /// Gets the name of the sector.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the owner of the sector.
    /// </summary>
    string Owner { get; }

    /// <summary>
    /// Gets the ships in the sector.
    /// </summary>
    IShipModelList Ships { get; }
}
