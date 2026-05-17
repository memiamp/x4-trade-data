using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Services;

/// <summary>
/// A class that implements a game resource model loader.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="serviceProvider">An <see cref="IServiceProvider"/> that is the service provider to use.</param>
internal class GameResourceModelLoader(
                                       ILogger<GameResourceModelLoader> logger,
                                       IServiceProvider serviceProvider)
    : IGameResourceModelLoader
{
    IGameResourceModels IGameResourceModelLoader.LoadModels(IGameResourceData data)
    {
        using var scopedServiceProvider = serviceProvider.CreateScope();
        var modelParser = serviceProvider.GetRequiredService<IModelParser>();

        var parsingScope = serviceProvider.GetRequiredService<IGameResourceModelParsingScope>();
        
        var texts = modelParser.Parse<ITextResourcePageDictionary, ITextResourceModelList>(data.Text);
        parsingScope.TextResources = texts;

        var colours = modelParser.Parse<IColourResourceData, IColourModelList>(data.Colours);
        parsingScope.Colours = colours;

        var factions = modelParser.Parse<IFactionDataDictionary, IFactionModelList>(data.Factions);

        var offsets = modelParser.Parse<IOffsetDataDictionary, IOffsetModelReference>(data.Offsets);

        var sectorNames = modelParser.Parse<IMacroNameResourceDataDictionary, IMacroNameModelList>(data.SectorNames);

        var shipModels = modelParser.Parse<IMacroNameResourceDataDictionary, IMacroNameModelList>(data.ShipModels);

        var wareNames = modelParser.Parse<IMacroNameResourceDataDictionary, IMacroNameModelList>(data.WareNames);

        return new GameResourceModels
        {
            Colours = colours,
            Factions = factions,
            Offsets = offsets,
            SectorNames = sectorNames,
            ShipModels = shipModels,
            Text = texts,
            WareNames = wareNames
        };
    }
}
