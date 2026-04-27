using System.Diagnostics.CodeAnalysis;
using MPL.X4.TradeData.Services;
using MPL.X4.TradeData.UI.Models;

namespace MPL.X4.TradeData.UI.Services;

/// <summary>
/// A class that implements a model mapper.
/// </summary>
internal class ModelMapper : IModelMapper
{
    ShipClass IModelMapper.MapShipClass(string source)
        => source switch
        {
            Constants.SaveGameFile.AttributeValue.ShipClass.ExtraLarge => ShipClass.ExtraLarge,
            Constants.SaveGameFile.AttributeValue.ShipClass.Large => ShipClass.Large,
            Constants.SaveGameFile.AttributeValue.ShipClass.Medium => ShipClass.Medium,
            Constants.SaveGameFile.AttributeValue.ShipClass.Small => ShipClass.Small,
            _ => ShipClass.Unknown
        };

    SpecialItem IModelMapper.MapSpecialItem(int sectorNameId, ILockbox source, IResourceData resourceData)
        => new()
        {
            Code = source.Code,
            Description = $"{source.Type} ({source.LockCount} locks)",
            SectorName = resourceData.SectorNames[sectorNameId],
            Type = SpecialItemType.Lockbox,
            X = $"{source.Position.X:0}",
            Y = $"{source.Position.Y:0}",
            Z = $"{source.Position.Z:0}"
        };

    SpecialItem IModelMapper.MapSpecialItem(int sectorNameId, IShip source, IResourceData resourceData)
        => new()
        {
            Code = source.Code,
            Description = $"{((IModelMapper)this).MapShipClass(source.Class)} - {source.Macro}",
            SectorName = resourceData.SectorNames[sectorNameId],
            Type = SpecialItemType.Ship,
            X = $"{source.Position.X:0}",
            Y = $"{source.Position.Y:0}",
            Z = $"{source.Position.Z:0}"
        };

    IEnumerable<SpecialItem> IModelMapper.MapSpecialItems(IEnumerable<ISector> source, IResourceData resourceData)
    {
        var returnValue = new List<SpecialItem>();

        var ships = source
                          .SelectMany(x => x.Ships,
                                      (sector, ship) => new
                                      {
                                          SectorNameId = sector.NameId,
                                          Ship = ship
                                      })
                          .Where(x => x.Ship.Owner == Constants.SaveGameFile.AttributeValue.Owner.Ownerless);
        if (ships?.Any() == true)
        {
            returnValue.AddRange(ships.Select(x => ((IModelMapper)this).MapSpecialItem(x.SectorNameId, x.Ship, resourceData)));
        }

        var lockboxes = source
                              .SelectMany(x => x.Lockboxes,
                                          (sector, lockbox) => new
                                          {
                                              SectorNameId = sector.NameId,
                                              Lockbox = lockbox
                                          });
        if (lockboxes?.Any() == true)
        {
            returnValue.AddRange(lockboxes.Select(x => ((IModelMapper)this).MapSpecialItem(x.SectorNameId, x.Lockbox, resourceData)));
        }

        return returnValue;
    }

    TradeOffer IModelMapper.MapTradeOffer(ISector sector, IStation station, ITrade trade, IResourceData resourceData)
    {
        var tradeType = (trade.AmountToBuy, trade.AmountToSell) switch
        {
            ( > 0, _) => TradeType.Buy,
            (_, > 0) => TradeType.Sell,
            _ => TradeType.Unknown
        };

        var amount = tradeType switch
        {
            TradeType.Buy => trade.AmountToBuy,
            TradeType.Sell => trade.AmountToSell,
            _ => 0
        };

        var sectorName = resourceData.SectorNames[sector.NameId];
        var stationName = station.NameId is not null
                                                     ? resourceData.Lookup(station.NameId)
                                                     : station.Code;

        return new TradeOffer()
        {
            Amount = amount,
            Price = trade.Price,
            SectorName = sectorName,
            StationName = stationName,
            Type = tradeType,
            Ware = trade.Ware
        };
    }

    IEnumerable<TradeOffer> IModelMapper.MapTradeOffers(IEnumerable<ISector> source, IResourceData resourceData)
        => source
                 .SelectMany(x => x.Stations,
                             (sector, station) => new
                             {
                                 Sector = sector,
                                 Station = station
                             })
                 .SelectMany(x => x.Station.Trades,
                             (a, trades) => new
                             {
                                 a.Sector,
                                 a.Station,
                                 Trade = trades
                             })
                 .Select(x => ((IModelMapper)this).MapTradeOffer(x.Sector, x.Station, x.Trade, resourceData))
           ?? [];
}
