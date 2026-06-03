using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IEconomyLogModel"/> from an <see cref="IEconomyLogData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class EconomyLogModelParser(
                                     ILogger<EconomyLogModelParser> logger,
                                     ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<IEconomyLogData, IEconomyLogModel>(logger)
{
    private protected override IEconomyLogModel OnParse(IEconomyLogData source)
    {
        var tradeLog = ParseTradeLog(source.TradeLog);

        return new EconomyLogModel
        {
            TradeLog = tradeLog
        };
    }

    private ITradeLogShipModel ParseShip(IShipModel source, ITradeLogShipModelList ships)
    {
        var returnValue = ships.FirstOrDefault(x => x.Id == source.Id);
        if (returnValue is null)
        {
            returnValue = new TradeLogShipModel
            {
                Code = source.Code,
                Id = source.Id,
                Name = source.Name ?? source.Model,
                Trades = new TradeLogTradeModelList()
            };

            ships.Add(returnValue);
        }

        return returnValue;
    }

    private ITradeLogShipModelList ParseTradeLog(ITradeLogData source)
    {
        var returnValue = new TradeLogShipModelList();
        var unknownIds = new List<string>();

        foreach (var item in source)
        {
            if (TryParseSourceShipAndStation(item, unknownIds, out var sourceShip, out var station, out var tradeType))
            {
                var ship = ParseShip(sourceShip, returnValue);

                var trade = ParseTradeLogTrade(item, station, tradeType);

                ship.Trades.Add(trade);
            }
        }

        return returnValue;
    }

    private ITradeLogTradeModel ParseTradeLogTrade(ITradeLogEntryData source, IStationModel station, TradeType tradeType)
    {
        var wareName = parsingScope.ParseWareName(source.Ware);

        return new TradeLogTradeModel
        {
            Amount = source.Volume,
            BoughtFrom = tradeType == TradeType.Buy ? station : null,
            Id = "",
            Name = wareName,
            Price = source.Price,
            SoldTo = tradeType == TradeType.Sell ? station : null,
            Type = tradeType
        };
    }

    private bool TryParseSourceShipAndStation(
                                              ITradeLogEntryData source,
                                              IList<string> unknownIds,
                                              [NotNullWhen(true)] out IShipModel? ship,
                                              [NotNullWhen(true)] out IStationModel? station,
                                              out TradeType tradeType)
    {
        ship = null;
        station = null;
        tradeType = TradeType.Unknown;

        if (unknownIds.Contains(source.BuyerId) ||
            unknownIds.Contains(source.SellerId))
        {
            return false;
        }

        ship = parsingScope.CurrentShips.FirstOrDefault(x => x.Id == source.BuyerId);
        if (ship is null)
        {
            ship = parsingScope.CurrentShips.FirstOrDefault(x => x.Id == source.SellerId);
            station = parsingScope.CurrentStations.FirstOrDefault(x => x.Id == source.BuyerId);

            if (ship is null)
            {
                Logger.LogWarning("Could not process log trade entry with identifier {Id}", source.SellerId);
                unknownIds.Add(source.SellerId);
            }
            if (station is null)
            {
                Logger.LogWarning("Could not process log trade entry with identifier {Id}", source.BuyerId);
                unknownIds.Add(source.BuyerId);
            }

            tradeType = TradeType.Sell;
        }
        else
        {
            station = parsingScope.CurrentStations.FirstOrDefault(x => x.Id == source.SellerId);

            if (station is null)
            {
                Logger.LogWarning("Could not process log trade entry with identifier {Id}", source.SellerId);
                unknownIds.Add(source.BuyerId);
            }

            tradeType = TradeType.Buy;
        }

        return ship is not null &&
               station is not null;
    }
}
