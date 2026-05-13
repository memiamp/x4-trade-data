using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IUniverseModel"/> from an <see cref="IUniverseData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser.</param>
internal class UniverseModelParser(
                                   ILogger<UniverseModelParser> logger,
                                   IModelParser modelParser)
    : ModelParserBase<IUniverseData, IUniverseModel>(logger)
{
    private protected override IUniverseModel OnParse(IUniverseData source)
    {
        var sectors = modelParser.Parse<IEnumerable<ISectorData>, ISectorModelList>(source.Sectors);

        return new UniverseModel
        {
            Sectors = sectors
        };
    }
}
