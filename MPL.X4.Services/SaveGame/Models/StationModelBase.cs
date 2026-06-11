namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements the base model of a station.
/// </summary>
internal class StationModelBase : SectorElementWithCargoModelBase, IHasTrades
{
    public required ITradeModelList Trades { get; init; }
}
