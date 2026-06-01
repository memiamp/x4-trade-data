using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="ITradeModelList"/> from an <see cref="IEnumerable{ITradeData}"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class TradeModelListParser(
                                    ILogger<TradeModelListParser> logger,
                                    ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<IEnumerable<ITradeData>, ITradeModelList>(logger)
{
    private protected override ITradeModelList OnParse(IEnumerable<ITradeData> source)
    {
        var returnValue = new TradeModelList();

        foreach (var item in source)
        {
            var models = ParseTradeModels(item);

            returnValue.AddRange(models);
        }

        return returnValue;
    }
    private IEnumerable<ITradeModel> ParseTradeModels(ITradeData source)
    {
        var returnValue = new List<ITradeModel>();

        var name = parsingScope.ParseWareName(source.Ware);

        if (source.AmountToBuy > 0)
        {
            returnValue.Add(new TradeModel
            {
                Amount = source.AmountToBuy,
                Id = source.Ware,
                Name = name,
                Price = source.Price,
                Type = TradeType.Buy
            });
        }
        
        if (source.AmountToSell > 0)
        {
            returnValue.Add(new TradeModel
            {
                Amount = source.AmountToSell,
                Id = source.Ware,
                Name = name,
                Price = source.Price,
                Type = TradeType.Sell
            });
        }

        return returnValue;
    }
}
