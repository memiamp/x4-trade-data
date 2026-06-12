using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;
using MPL.X4.SaveGame.Models.Services;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="ISaveGameModels"/> from an <see cref="ISaveGameData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class SaveGameModelsParser(
                                    ILogger<SaveGameModelsParser> logger,
                                    IModelParser modelParser,
                                    ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<ISaveGameData, ISaveGameModels>(logger)
{
    private protected override ISaveGameModels OnParse(ISaveGameData source)
    {
        var universe = modelParser.Parse<IUniverseData, IUniverseModel>(source.Universe);
        
        parsingScope.CurrentBuildStorageModels = universe
                                                         .Sectors
                                                         .SelectMany(x => x.BuildStorages);
        parsingScope.CurrentShipModels = universe.GetAllShips();
        parsingScope.CurrentStationModels = universe
                                                    .Sectors
                                                    .SelectMany(x => x.Stations);
        parsingScope.GameTime = source.Information.Game.Time;

        var economyLog = modelParser.Parse<IEconomyLogData, IEconomyLogModel>(source.EconomyLog);

        return new SaveGameModels
        {
            EconomyLog = economyLog,
            Universe = universe
        };
    }
}
