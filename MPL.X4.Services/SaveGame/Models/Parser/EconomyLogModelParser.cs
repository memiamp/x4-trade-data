using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IEconomyLogModel"/> from an <see cref="IEconomyLogData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class EconomyLogModelParser(
                                     ILogger<EconomyLogModelParser> logger,
                                     ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<IEconomyLogData, IEconomyLogModel>(logger)
{
    private readonly Lock _parseLock = new();
    private IEnumerable<IRemovedObjectData> _removedObjects = [];
    private Dictionary<string, ITradePartnerModel> _tradePartnerCache = [];

    private protected override IEconomyLogModel OnParse(IEconomyLogData source)
    {
        logger.LogInformation("Parsing economy log model");

        lock (_parseLock)
        {
            _removedObjects = source.RemovedObjects;
            _tradePartnerCache = [];

            var tradeLog = ParseTradeLog(source.TradeLog);

            return new EconomyLogModel
            {
                TradeLog = tradeLog
            };
        }
    }

    private TradeLogEntryModelList ParseTradeLog(ITradeLogData source)
    {
        var returnValue = new TradeLogEntryModelList();

        foreach (var item in source)
        {
            if (TryParseTradeLogEntry(item, out var entry))
            {
                returnValue.Add(entry);
            }
        }

        return returnValue;
    }

    private bool TryFindTradePartner(string id, [NotNullWhen(true)] out ITradePartnerModel? partner)
    {
        if (!_tradePartnerCache.TryGetValue(id, out partner))
        {
            if (!TryFindTradePartnerRemoved(id, out partner) &&
                !TryFindTradePartnerShip(id, out partner) &&
                !TryFindTradePartnerStation(id, out partner) &&
                !TryFindTradePartnerBuildStorage(id, out partner))
            {
                Logger.LogWarning("Unable to located trade partner for {TradePartnerId}", id);
            }
            else
            {
                _tradePartnerCache.Add(id, partner);
            }
        }

        return partner is not null;
    }

    private bool TryFindTradePartnerBuildStorage(string id, [NotNullWhen(true)] out ITradePartnerModel? partner)
    {
        partner = null;

        var buildStorageModel = parsingScope.CurrentBuildStorageModels.FirstOrDefault(x => x.Id == id);
        if (buildStorageModel is not null)
        {
            partner = new TradePartnerBuildStorageModel(buildStorageModel);
        }
        else
        {
            var stationModel = parsingScope.CurrentStationModels.FirstOrDefault(x => x.BuildStorage?.Id == id);
            if (stationModel is not null)
            {
                partner = new TradePartnerStationBuildStorageModel(stationModel);
            }
        }

        return partner is not null;
    }

    private bool TryFindTradePartnerRemoved(string id, [NotNullWhen(true)] out ITradePartnerModel? partner)
    {
        partner = null;

        var removedObject = _removedObjects.FirstOrDefault(x => x.Id == id);
        if (removedObject is not null)
        {
            var owner = parsingScope.ParseFaction(removedObject.Owner);
            partner = new TradePartnerRemovedModel(owner, removedObject);
        }

        return partner is not null;
    }

    private bool TryFindTradePartners(ITradeLogEntryData source, [NotNullWhen(true)] out ITradePartnerModel? buyer, [NotNullWhen(true)] out ITradePartnerModel? seller)
    {
        var hasBuyer = TryFindTradePartner(source.BuyerId, out buyer);
        var hasSeller = TryFindTradePartner(source.SellerId, out seller);

        if (!hasBuyer && !hasSeller)
        {
            Logger.LogWarning("Unable to locate buyer with {BuyerId} and seller with {SellerId} for trade log entry at {TimeIndex}", source.BuyerId, source.SellerId, source.Time);
        }
        else if (!hasBuyer)
        {
            Logger.LogWarning("Unable to locate buyer with {BuyerId} for trade log entry at {TimeIndex}", source.BuyerId, source.Time);
        }
        else if (!hasSeller)
        {
            Logger.LogWarning("Unable to locate seller with {SellerId} for trade log entry at {TimeIndex}", source.SellerId, source.Time);
        }

        return buyer is not null &&
               seller is not null;
    }

    private bool TryFindTradePartnerShip(string id, [NotNullWhen(true)] out ITradePartnerModel? partner)
    {
        partner = null;

        var ship = parsingScope.CurrentShipModels.FirstOrDefault(x => x.Id == id);
        if (ship is not null)
        {
            partner = new TradePartnerShipModel(ship);
        }

        return partner is not null;
    }

    private bool TryFindTradePartnerStation(string id, [NotNullWhen(true)] out ITradePartnerModel? partner)
    {
        partner = null;

        var stationModel = parsingScope.CurrentStationModels.FirstOrDefault(x => x.Id == id);
        if (stationModel is not null)
        {
            partner = new TradePartnerStationModel(stationModel);
        }

        return partner is not null;
    }

    private bool TryParseTradeLogEntry(ITradeLogEntryData source, [NotNullWhen(true)] out ITradeLogEntryModel? entry)
    {
        entry = null;

        if (TryFindTradePartners(source, out var buyer, out var seller))
        {
            var wareName = parsingScope.ParseWareName(source.Ware);

            var tradeAge = parsingScope.GameTime - source.Time;

            entry = new TradeLogEntryModel
            {
                Amount = source.Volume,
                Buyer = buyer,
                Id = source.Time.ToString(),
                Name = wareName,
                Price = source.Price,
                Seller = seller,
                Time = source.Time,
                TradeAge = TimeSpan.FromSeconds(tradeAge),
                Type = TradeType.Unknown
            };
        }

        return entry is not null;
    }
}
