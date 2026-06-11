using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a scope for parsing save game models.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class SaveGameModelParsingScope(
                                         ILogger<SaveGameModelParsingScope> logger)
    : ISaveGameModelParsingScope
{
    IFactionModel ISaveGameModelParsingScope.ParseFaction(string source)
    {
        if (((ISaveGameModelParsingScope)this).GameResources?.Factions.TryGetValue(source, out var returnValue) == true)
        {
            return returnValue;
        }

        logger.LogDebug("Could not find faction {Faction} in factions list", source);
        throw new ArgumentException("A faction {Faction} could not be parsed", source);
    }

    string ISaveGameModelParsingScope.ParseWareName(string source, string defaultResult)
    {
        if (!((ISaveGameModelParsingScope)this).TryParseWareName(source, out var returnValue))
        {
            logger.LogDebug("Could not find ware name {Ware} in factions list", source);

            returnValue = defaultResult;
        }

        return returnValue;
    }

    bool ISaveGameModelParsingScope.TryParseWareName(string source, [NotNullWhen(true)] out string? wareName)
    {
        var returnValue = false;
        wareName = null;

        if (((ISaveGameModelParsingScope)this).GameResources?.Wares.TryGetValue(source, out var model) == true)
        {
            wareName = model.Name;
            returnValue = true;
        }

        return returnValue;
    }

    IEnumerable<IBuildStorageData> ISaveGameModelParsingScope.CurrentBuildStorages { get; set; } = [];

    IEnumerable<IBuildStorageModel> ISaveGameModelParsingScope.CurrentBuildStorageModels { get; set; } = [];

    ITransform3D ISaveGameModelParsingScope.CurrentOffset { get; set; } = ITransform3D.GetDefault();

    IEnumerable<IShipModel> ISaveGameModelParsingScope.CurrentShipModels { get; set; } = [];

    IEnumerable<IStationModel> ISaveGameModelParsingScope.CurrentStationModels { get; set; } = [];

    [AllowNull]
    IGameResourceModels ISaveGameModelParsingScope.GameResources { get; set; }

    double ISaveGameModelParsingScope.GameTime { get; set; }
}
