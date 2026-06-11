namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a station.
/// </summary>
public interface IStationData : IHasIsKnown, IHasState, IHasTransform
{
    /// <summary>
    /// Gets the base name resource of the station (if any).
    /// </summary>
    ITextResourceReference? BaseNameResource { get; }

    /// <summary>
    /// Gets the building module connection identifier for this station.
    /// </summary>
    string? BuildingModuleConnectionId { get; }

    /// <summary>
    /// Gets the building module identifier for this station.
    /// </summary>
    string? BuildingModuleId { get; }

    /// <summary>
    /// Gets the station cargo.
    /// </summary>
    ICargoData Cargo { get; }

    /// <summary>
    /// Gets the code of the station.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the count of defence modules at the station.
    /// </summary>
    int DefenceModuleCount { get; }

    /// <summary>
    /// Gets the identifier of the station.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the macro of the station.
    /// </summary>
    string Macro { get; }

    /// <summary>
    /// Gets the defined text name of the station (if any).
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// Gets the name resource of the station (if any).
    /// </summary>
    ITextResourceReference? NameResource { get; }

    /// <summary>
    /// Gets the name index of the station (i.e. I, II, III, etc as an arabic number).
    /// </summary>
    int NameIndex { get; }

    /// <summary>
    /// Gets the owner of the station.
    /// </summary>
    string Owner { get; }

    /// <summary>
    /// Gets the productions at the station.
    /// </summary>
    IEnumerable<string> Productions { get; }

    /// <summary>
    /// Gets the ships docked at the station.
    /// </summary>
    IEnumerable<IShipData> Ships { get; }

    /// <summary>
    /// Gets the trades on offer at the station.
    /// </summary>
    IEnumerable<ITradeData> Trades { get; }
}
