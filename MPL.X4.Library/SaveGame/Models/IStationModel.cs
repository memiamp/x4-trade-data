namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a station.
/// </summary>
public interface IStationModel : IHasCode, IHasCargo, IHasIsKnown, IHasOwner, IHasShips, IHasTrades, IHasTransform, IIsWreckable, IModelWithId
{
    /// <summary>
    /// Gets the build storage for this station.
    /// </summary>
    IBuildStorageModel? BuildStorage { get; }

    /// <summary>
    /// Gets an indication of whether the station is abandoned.
    /// </summary>
    bool IsAbandoned { get; }

    /// <summary>
    /// Gets an indication of whether this station is being constructed.
    /// </summary>
    bool IsUnderConstruction { get; }

    /// <summary>
    /// Gets the name of the station.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the productions at the station.
    /// </summary>
    IProductionModelList Productions { get; }
}
