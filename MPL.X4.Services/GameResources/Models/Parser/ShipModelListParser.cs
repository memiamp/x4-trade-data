using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="IMacroNameModelList"/> from an <see cref="IShipModelResourceDataDictionary"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
internal class ShipModelListParser(
                                   ILogger<ShipModelListParser> logger,
                                   IModelParser modelParser)
    : ModelParserBase<IShipModelResourceDataDictionary, IMacroNameModelList>(logger),
      IModelParser<IShipModelResourceDataDictionary, IMacroNameModelList>
{
    private protected override IMacroNameModelList OnParse(IShipModelResourceDataDictionary source)
    {
        var returnValue = new MacroNameModelList();

        var models = modelParser.Parse<IMacroNameResourceDataDictionary, IMacroNameModelList>(source);
        returnValue.AddRange(models);

        foreach (var (macro, alias) in source.Aliases)
        {
            if (models.TryGetValue(alias, out var model))
            {
                returnValue.Add(new MacroNameModel
                {
                    Id = macro,
                    Name = model.Name
                });
            }
        }

        return returnValue;
    }
}
