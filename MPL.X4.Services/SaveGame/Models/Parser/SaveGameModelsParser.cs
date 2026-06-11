using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

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
    private static IEnumerable<IShipModel> FlattenShips(IEnumerable<IShipModel> source)
    {
        foreach (var ship in source)
        {
            yield return ship;
            foreach (var child in FlattenShips(ship.Ships))
            {
                yield return child;
            }
        }
    }

    private static IEnumerable<IShipModel> GetAllShips(IUniverseModel source)
    {
        var sectors = source.Sectors;

        var sectorShips = sectors.SelectMany(x => x.Ships);

        var stations = sectors.SelectMany(x => x.Stations);
        var stationShips = stations.SelectMany(x => x.Ships);

        var buildStorages = stations
                                    .Where(x => x.BuildStorage is not null)
                                    .Select(x => x.BuildStorage!);
        var buildStorageShips = buildStorages.SelectMany(x => x.Ships);

        var ships = sectorShips
                               .Concat(stationShips)
                               .Concat(buildStorageShips);

        return FlattenShips(ships);
    }

    private protected override ISaveGameModels OnParse(ISaveGameData source)
    {
        var universe = modelParser.Parse<IUniverseData, IUniverseModel>(source.Universe);

        parsingScope.CurrentBuildStorageModels = universe
                                                         .Sectors
                                                         .SelectMany(x => x.BuildStorages);
        parsingScope.CurrentShipModels = GetAllShips(universe);
        parsingScope.CurrentStationModels = universe
                                                    .Sectors
                                                    .SelectMany(x => x.Stations);

        var economyLog = modelParser.Parse<IEconomyLogData, IEconomyLogModel>(source.EconomyLog);

        return new SaveGameModels
        {
            EconomyLog = economyLog,
            Universe = universe
        };
    }
}
