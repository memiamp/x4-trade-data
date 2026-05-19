namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a station.
/// </summary>
public interface IStationModel : IHasIsKnown, IHasOwner, IHasTransform, IModelWithId
{
    /// <summary>
    /// Gets the code of the station.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets an indication of whether the station is abandoned.
    /// </summary>
    bool IsAbandoned { get; }

    /// <summary>
    /// Gets an indication of whether this station is being constructed.
    /// </summary>
    bool IsUnderConstruction { get; }

    /// <summary>
    /// Gets an indication of whether the station is a wreck.
    /// </summary>
    bool IsWreck { get; }

    /// <summary>
    /// Gets the name of the station.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the productions at the station.
    /// </summary>
    IProductionModelList Productions { get; }

    /// <summary>
    /// Gets the trades on offer at the station.
    /// </summary>
    ITradeModelList Trades { get; }
}
