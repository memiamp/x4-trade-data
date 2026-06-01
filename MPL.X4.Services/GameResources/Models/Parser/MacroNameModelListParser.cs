using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="IMacroNameModelList"/> from an <see cref="IMacroNameResourceDataDictionary"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
internal class MacroNameModelListParser(
                                        ILogger<MacroNameModelListParser> logger,
                                        IModelParser modelParser)
    : ModelListParser<IMacroNameResourceData, IMacroNameResourceDataDictionary, string, IMacroNameModel, IMacroNameModelList>(logger, modelParser)
{
    private protected override IMacroNameModelList CreateTarget()
        => new MacroNameModelList();
}
