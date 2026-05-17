using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="ICargoItemModel"/> from an <see cref="IWareItemData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class CargoItemModelParser(
                                    ILogger<CargoItemModelParser> logger,
                                    ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<IWareItemData, ICargoItemModel>(logger)
{
    private protected override ICargoItemModel OnParse(IWareItemData source)
    {
        var name = ParseName(source);

        return new CargoItemModel
        {
            Amount = source.Amount,
            Id = source.Ware,
            Name = name
        };
    }

    private string ParseName(IWareItemData source)
    {
        var returnValue = string.Empty;
        if (parsingScope.GameResources.WareNames.TryGetValue(source.Ware, out var model))
        {
            returnValue = model.Name;
        }

        return returnValue;
    }
}
