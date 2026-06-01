using Microsoft.Extensions.Logging;
using MPL.X4.Parser;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="IProductionModelList"/> from an <see cref="IEnumerable{string}"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class ProductionModelListParser(
                                         ILogger<ProductionModelListParser> logger,
                                         ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<IEnumerable<string>, IProductionModelList>(logger)
{
    private protected override IProductionModelList OnParse(IEnumerable<string> source)
    {
        var returnValue = new ProductionModelList();

        var productions = source
                                .GroupBy(x => x)
                                .ToDictionary(
                                              x => x.Key,
                                              x => x.Count());
        foreach (var item in productions)
        {
            var name = parsingScope.ParseWareName(item.Key);

            returnValue.Add(new ProductionModel
            {
                Id = item.Key,
                ModuleCount = item.Value,
                Name = name
            });
        }

        return returnValue;
    }
}
