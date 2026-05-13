using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="ISaveGameModels"/> from an <see cref="ISaveGameData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser.</param>
internal class SaveGameModelsParser(
                                    ILogger<SaveGameModelsParser> logger,
                                    IModelParser modelParser)
    : ModelParserBase<ISaveGameData, ISaveGameModels>(logger)
{
    private protected override ISaveGameModels OnParse(ISaveGameData source)
    {
        var universe = modelParser.Parse<IUniverseData, IUniverseModel>(source.Universe);

        return new SaveGameModels
        {
            Universe = universe
        };
    }
}
