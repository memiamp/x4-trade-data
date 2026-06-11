using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IBuildStorageModel"/> from an <see cref="IBuildStorageData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class BuildStorageModelParser(
                                       ILogger<BuildStorageModelParser> logger,
                                       IModelParser modelParser,
                                       ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<IBuildStorageData, IBuildStorageModel>(logger)
{
    private protected override IBuildStorageModel OnParse(IBuildStorageData source)
    {
        var cargo = modelParser.Parse<IEnumerable<IWareItemData>, ICargoItemModelList>(source.Cargo.Items);
        var owner = parsingScope.ParseFaction(source.Owner);
        var ships = ParseShips(source.Ships);
        var trades = modelParser.Parse<IEnumerable<ITradeData>, ITradeModelList>(source.Trades);
        var transform = source.Transform.Add(parsingScope.CurrentOffset);

        return new BuildStorageModel
        {
            Cargo = cargo,
            Code = source.Code,
            IsKnown = source.IsKnown,
            Id = source.Id,
            IsWreck = source.State == Constants.XmlDataFile.AttributeValue.State.Wreck,
            Owner = owner,
            Ships = ships,
            Trades = trades,
            Transform = transform,
        };
    }

    private IShipModelList ParseShips(IEnumerable<IShipData> source)
    {
        var returnValue = new ShipModelList();

        foreach (var item in source)
        {
            var model = modelParser.Parse<IShipData, IShipModel>(item);
            returnValue.Add(model);
        }

        return returnValue;
    }
}
