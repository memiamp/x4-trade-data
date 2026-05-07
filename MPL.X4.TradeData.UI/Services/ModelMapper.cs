using MPL.X4.GameResources.Data;
using MPL.X4.TradeData.UI.Models;

namespace MPL.X4.TradeData.UI.Services;

/// <summary>
/// A class that implements a model mapper.
/// </summary>
internal class ModelMapper : IModelMapper
{
    Sector IModelMapper.MapSector(ISector source, IGameResourceData resourceData)
        => new()
        {
            AbandonedShipCount = source.Ships.Count(x => x.Owner == Constants.SaveGameFile.AttributeValue.Owner.Ownerless),
            Code = source.Code,
            LockboxCount = source.Lockboxes.Count(),
            Name = source.Macro,
            //Name = resourceData.LookupSectorNameFromMacro(source.Macro),
            ShipCount = source.Ships.Count(),
            StationCount = source.Stations.Count()
        };

    IEnumerable<Sector> IModelMapper.MapSectors(IEnumerable<ISector> source, IGameResourceData resourceData)
        => source.Select(x => ((IModelMapper)this).MapSector(x, resourceData));

    ShipClass IModelMapper.MapShipClass(string source)
        => source switch
        {
            Constants.SaveGameFile.AttributeValue.ShipClass.ExtraLarge => ShipClass.ExtraLarge,
            Constants.SaveGameFile.AttributeValue.ShipClass.Large => ShipClass.Large,
            Constants.SaveGameFile.AttributeValue.ShipClass.Medium => ShipClass.Medium,
            Constants.SaveGameFile.AttributeValue.ShipClass.Small => ShipClass.Small,
            _ => ShipClass.Unknown
        };

    SpecialItem IModelMapper.MapSpecialItem(string sectorMacro, ILockbox source, IGameResourceData resourceData)
    {
        var comments = $"Lock count: {source.LockCount}";

        if (source.Wares.Any())
        {
            comments += $", Contents: {string.Join(", ", source.Wares)}";
        }

        return new()
        {
            Code = source.Code,
            Comments = comments,
            Description = $"{source.Type}",
            SectorName = sectorMacro,
            //SectorName = resourceData.LookupSectorNameFromMacro(sectorMacro),
            Type = SpecialItemType.Lockbox,
            X = $"{source.Position.X:0}",
            Y = $"{source.Position.Y:0}",
            Z = $"{source.Position.Z:0}"
        };
    }

    SpecialItem IModelMapper.MapSpecialItem(string sectorMacro, IShip source, IGameResourceData resourceData)
    {
        var comments = string.Empty;

        if (source.Cargo.Items.Any())
        {
            comments = string.Join(", ", source.Cargo.Items.Select(x => x.ToString()));
        }

        if (source.Modifications.Any())
        {
            var modifications = $"{source.Modifications.Count()}: {string.Join(", ", source.Modifications)}";
            if (comments.Length > 0)
            {
                comments += $", {modifications}";
            }
            else
            {
                comments = modifications;
            }
        }

        return new SpecialItem
        {
            Code = source.Code,
            Comments = comments,
            Description = $"{((IModelMapper)this).MapShipClass(source.Class)} - {source.Macro}",
            SectorName = sectorMacro,
            //SectorName = resourceData.LookupSectorNameFromMacro(sectorMacro),
            Type = SpecialItemType.Ship,
            X = $"{source.Position.X:0}",
            Y = $"{source.Position.Y:0}",
            Z = $"{source.Position.Z:0}"
        };
    }

    IEnumerable<SpecialItem> IModelMapper.MapSpecialItems(IEnumerable<ISector> source, IGameResourceData resourceData)
    {
        var returnValue = new List<SpecialItem>();

        var ships = source
                          .SelectMany(x => x.Ships,
                                      (sector, ship) => new
                                      {
                                          SectorMacro = sector.Macro,
                                          Ship = ship
                                      })
                          .Where(x => x.Ship.Owner == Constants.SaveGameFile.AttributeValue.Owner.Ownerless);
        if (ships?.Any() == true)
        {
            returnValue.AddRange(ships.Select(x => ((IModelMapper)this).MapSpecialItem(x.SectorMacro, x.Ship, resourceData)));
        }

        var lockboxes = source
                              .SelectMany(x => x.Lockboxes,
                                          (sector, lockbox) => new
                                          {
                                              SectorMacro = sector.Macro,
                                              Lockbox = lockbox
                                          });
        if (lockboxes?.Any() == true)
        {
            returnValue.AddRange(lockboxes.Select(x => ((IModelMapper)this).MapSpecialItem(x.SectorMacro, x.Lockbox, resourceData)));
        }

        return returnValue;
    }

    TradeOffer IModelMapper.MapTradeOffer(ISector sector, IStation station, ITrade trade, IGameResourceData resourceData)
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

        var sectorName = sector.Macro;
        //var sectorName = resourceData.LookupSectorNameFromMacro(sector.Macro);
        var stationName = station.NameId is not null
                                                     ? station.NameId?.ToString() ?? station.Code
                                                     //? resourceData.Lookup(station.NameId)
                                                     : station.Code;

        return new TradeOffer()
        {
            Amount = amount,
            Price = trade.Price,
            SectorName = sectorName,
            SectorOwner = sector.Owner,
            StationOwner = station.Owner,
            StationName = stationName,
            Type = tradeType,
            Ware = trade.Ware
        };
    }

    IEnumerable<TradeOffer> IModelMapper.MapTradeOffers(IEnumerable<ISector> source, IGameResourceData resourceData)
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
