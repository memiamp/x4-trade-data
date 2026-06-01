using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="IModificationModelList"/> from an <see cref="IEnumerable{IWareItemData}"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
internal class ModificationModelListParser(
                                           ILogger<ModificationModelListParser> logger,
                                           IModelParser modelParser)
    : ModelListParser<IModificationData, string, IModificationModel, IModificationModelList>(logger, modelParser)
{
    private protected override IModificationModelList CreateTarget()
        => new ModificationModelList();
}
