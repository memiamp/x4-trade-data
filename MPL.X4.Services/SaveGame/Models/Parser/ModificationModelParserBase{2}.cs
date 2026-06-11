using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <typeparamref name="TModel"/> from a <typeparamref name="TData"/>.
/// </summary>
/// <typeparam name="TData">The type of the modification data, which must implement <see cref="IModificationData"/>.</typeparam>
/// <typeparam name="TModel">The type of the modification model, which must implement <see cref="IModificationModel"/>.</typeparam>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal abstract class ModificationModelParserBase<TData, TModel>(
                                                                   ILogger<ModificationModelParserBase<TData, TModel>> logger,
                                                                   ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<TData, TModel>(logger)
    where TData : IModificationData
    where TModel : IModificationModel
{
    private protected void ParseNameAndQuality(string ware, out string name, out ModificationQuality quality)
    {
        name = parsingScope.ParseWareName(ware);
        if (name.Contains("\\\\"))
        {
            Console.WriteLine("YO");
        }
        quality = name.Count(c => c == Constants.Wares.ModificationQualityIndicator) switch
        {
            1 => ModificationQuality.Basic,
            2 => ModificationQuality.Enhanced,
            3 => ModificationQuality.Exceptional,
            _ => ModificationQuality.Basic
        };
    }
}
