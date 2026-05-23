using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="IWareModelList"/> from an <see cref="IWareDataDictionary"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
internal class WareModelListParser(
                                   ILogger<WareModelListParser> logger,
                                   IModelParser modelParser)
    : ModelListParser<IWareData, IWareDataDictionary, string, IWareModel, IWareModelList>(logger, modelParser)
{
    private protected override IWareModelList CreateTarget()
        => new WareModelList();
}
