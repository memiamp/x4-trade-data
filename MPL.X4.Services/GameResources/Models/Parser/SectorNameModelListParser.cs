using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="ISectorNameModelList"/> from an <see cref="ISectorNameDataDictionary"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
internal class SectorNameModelListParser(
                                          ILogger<SectorNameModelListParser> logger,
                                          IModelParser modelParser)
    : ModelListParser<ISectorNameData, ISectorNameDataDictionary, string, ISectorNameModel, ISectorNameModelList>(logger, modelParser)
{
    private protected override ISectorNameModelList CreateTarget()
        => new SectorNameModelList();
}
