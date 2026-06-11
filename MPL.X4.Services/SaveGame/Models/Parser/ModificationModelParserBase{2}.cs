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
    private static string _basicQuality = string.Empty;
    private static string _enhancedQuality = string.Empty;
    private static string _exceptionalQuality = string.Empty;
    private static bool _hasLoadedResource = false;

    private void EnsureResourcesLoaded()
    {
        if (!_hasLoadedResource)
        {
            if (parsingScope.GameResources.Text.TryGetValue(Constants.TextResource.ShipModifications.BasicQuality, out var resource))
            {
                _basicQuality = resource.Text;
            }

            if (parsingScope.GameResources.Text.TryGetValue(Constants.TextResource.ShipModifications.EnhancedQuality, out resource))
            {
                _enhancedQuality = resource.Text;
            }

            if (parsingScope.GameResources.Text.TryGetValue(Constants.TextResource.ShipModifications.ExceptionalQuality, out resource))
            {
                _exceptionalQuality = resource.Text;
            }

            _hasLoadedResource = true;
        }
    }

    private protected void ParseNameAndQuality(string ware, out string name, out ModificationQuality quality)
    {
        quality = ModificationQuality.Unknown;

        name = parsingScope.ParseWareName(ware);

        if (this is PaintModificationModelParser)
        {
            quality = ModificationQuality.Paint;
        }
        else
        {
            EnsureResourcesLoaded();

            if (name.Contains(_basicQuality))
            {
                quality = ModificationQuality.Basic;
            }
            else if (name.Contains(_enhancedQuality))
            {
                quality = ModificationQuality.Enhanced;
            }
            else if (name.Contains(_exceptionalQuality))
            {
                quality = ModificationQuality.Exceptional;
            }
        }

        if (quality == ModificationQuality.Unknown)
        {
            Logger.LogWarning("Modification {ModificationName} does not have a parseable quality", name);
        }
    }
}
