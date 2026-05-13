using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="ISectorModelList"/> from an <see cref="IEnumerable{ISectorData}"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
internal class SectorModelListParser(
                                     ILogger<SectorModelListParser> logger,
                                     IModelParser modelParser)
    : ModelListParser<ISectorData, string, ISectorModel, ISectorModelList>(logger, modelParser)
{
    private protected override ISectorModelList CreateTarget()
        => new SectorModelList();
}
