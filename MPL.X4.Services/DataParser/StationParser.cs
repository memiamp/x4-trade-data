using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services.DataParser;

/// <summary>
/// A class that implements a data parser for a <see cref="IStation"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="positionParser">An <see cref="IDataParser{ISectorPosition}"/> that is the position parser to use.</param>
/// <param name="tradeParser">An <see cref="IDataParser{ITrade}"/> that is the trade parser to use.</param>
internal class StationParser(
                             ILogger<StationParser> logger,
                             IDataParser<ISectorPosition> positionParser,
                             IDataParser<ITrade> tradeParser)
    : PositionalDataParserBase<IStation>(logger, positionParser)
{
    private async Task<IEnumerable<ITrade>> LoadProductionTrades(IXmlReaderWrapper reader)
    {
        List<ITrade> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Trade, XmlNodeType.Element))
            {
                var trade = await tradeParser.Parse(reader);
                returnValue.Add(trade);
            }
        }

        return returnValue;
    }

    private async Task<IEnumerable<ITrade>> LoadTrades(IXmlReaderWrapper reader)
    {
        IEnumerable<ITrade> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Production, XmlNodeType.Element, 2))
            {
                returnValue = await LoadProductionTrades(reader);
                break;
            }
        }

        return returnValue;
    }

    private protected override async Task<IStation> OnParse(IXmlReaderWrapper reader, ISectorPosition positionOffset)
    {
        SectorPosition position = new(positionOffset);
        Station? returnValue;
        List<ITrade> trades = [];

        if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Owner, out string? owner))
        {
            var isKnown = GetIsKnownToPlayer(reader);

            var nameId = reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.BaseName, out string? baseName)
                ? new TextResourceReference(baseName)
                : null;

            returnValue = new Station
            {
                Code = code,
                Id = id,
                IsKnown = isKnown,
                NameId = nameId,
                Owner = owner,
                Position = position,
                Trades = trades
            };
        }
        else
        {
            logger.LogWarning("Could not load station");
            throw new ArgumentException("Could not load station", nameof(reader));
        }

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Position, XmlNodeType.Element, 2))
            {
                await UpdatePosition(reader, position);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Trade, XmlNodeType.Element, 1))
            {
                using var tradesSubtree = await reader.ReadSubtree();

                var loadedTrades = await LoadTrades(tradesSubtree);
                trades.AddRange(loadedTrades);
            }
        }

        return returnValue;
    }
}
