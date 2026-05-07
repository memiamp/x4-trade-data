using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="IFactionModelList"/> from an <see cref="IFactionDataDictionary"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
internal class FactionModelListParser(
                                      ILogger<FactionModelListParser> logger,
                                      IModelParser modelParser)
    : ModelListParser<IFactionData, IFactionDataDictionary, string, IFactionModel, IFactionModelList>(logger, modelParser)
{
    private protected override IFactionModelList CreateTarget()
        => new FactionModelList();
}
