using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="ISectorModel"/> from an <see cref="ISectorData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class SectorModelParser(
                                 ILogger<SectorModelParser> logger,
                                 IModelParser modelParser,
                                 ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<ISectorData, ISectorModel>(logger),
      IModelParser<ISectorData, ISectorModel>
{
    private protected override ISectorModel OnParse(ISectorData source)
    {
        var name = ParseName(source);
        
        Logger.LogInformation("Parsing sector model {Name} ({Code})", name, source.Code);

        var buildStorages = new BuildStorageModelList();
        var collectableDrops = new CollectableDropModelList();
        var gates = new GateModelList();
        var lockboxes = new LockboxModelList();
        var ships = new ShipModelList();
        var stations = new StationModelList();

        var owner = parsingScope.ParseFaction(source.Owner);
        var transform = ParseTransform(source);

        ParseHighways(source, ships);
        ParseZones(source, buildStorages, collectableDrops, gates, lockboxes, ships, stations);

        return new SectorModel
        {
            BuildStorages = buildStorages,
            Code = source.Code,
            CollectableDrops = collectableDrops,
            Gates = gates,
            IsKnown = source.IsKnown,
            Id = source.Id,
            Lockboxes = lockboxes,
            Name = name,
            Owner = owner,
            Ships = ships,
            Stations = stations,
            Transform = transform
        };
    }

    private void ParseHighway(IHighwayData source, IShipModelList ships)
    {
        parsingScope.CurrentOffset = ITransform3D.GetDefault();

        foreach (var item in source.Ships)
        {
            var model = modelParser.Parse<IShipData, IShipModel>(item);
            ships.Add(model);
        }
    }

    private void ParseHighways(ISectorData source, IShipModelList ships)
    {
        foreach (var item in source.Highways)
        {
            ParseHighway(item, ships);
        }
    }

    private string ParseName(ISectorData source)
    {
        var returnValue = string.Empty;
        if (parsingScope.GameResources.SectorNames.TryGetValue(source.Macro, out var model))
        {
            returnValue = model.Name;
        }

        return returnValue;
    }

    private ITransform3D ParseTransform(ISectorData source)
        => parsingScope
                       .GameResources
                       .Offsets
                       .Sectors
                       .TryGetValue(source.Macro, out var offset)
                                                                  ? offset.Offset
                                                                  : ITransform3D.GetDefault();

    private void ParseZone(IZoneData source, IBuildStorageModelList buildStorages, ICollectableDropModelList collectableDrops, IGateModelList gates, ILockboxModelList lockboxes, IShipModelList ships, IStationModelList stations)
    {
        var zoneOffset = parsingScope
                                     .GameResources
                                     .Offsets
                                     .Zones
                                     .TryGetValue(source.Macro, out var offset)
                                                                                ? offset.Offset
                                                                                : ITransform3D.GetDefault();

        parsingScope.CurrentBuildStorages = source.BuildStorages;
        parsingScope.CurrentOffset = zoneOffset;

        foreach (var item in source.CollectableAmmos)
        {
            var models = modelParser.Parse<ICollectableAmmoData, ICollectableDropModelList>(item);
            collectableDrops.AddRange(models);
        }

        foreach (var item in source.CollectableWares)
        {
            var models = modelParser.Parse<ICollectableWareData, ICollectableDropModelList>(item);
            collectableDrops.AddRange(models);
        }

        foreach (var item in source.Gates)
        {
            var model = modelParser.Parse<IGateData, IGateModel>(item);
            gates.Add(model);
        }

        foreach (var item in source.Lockboxes)
        {
            var model = modelParser.Parse<ILockboxData, ILockboxModel>(item);
            lockboxes.Add(model);
        }

        foreach (var item in source.Ships)
        {
            var model = modelParser.Parse<IShipData, IShipModel>(item);
            ships.Add(model);
        }

        foreach (var item in source.Stations)
        {
            var model = modelParser.Parse<IStationData, IStationModel>(item);
            stations.Add(model);
        }

        var processedBuildStorages = stations
                                             .Where(x => x.BuildStorage is not null)
                                             .Select(x => x.BuildStorage!.Id);
        foreach (var item in source.BuildStorages)
        {
            if (!processedBuildStorages.Contains(item.Id))
            {
                var model = modelParser.Parse<IBuildStorageData, IBuildStorageModel>(item);
                buildStorages.Add(model);
            }
        }
    }

    private void ParseZones(ISectorData source, IBuildStorageModelList buildStorages, ICollectableDropModelList collectableDrops, IGateModelList gates, ILockboxModelList lockboxes, IShipModelList ships, IStationModelList stations)
    {
        foreach (var zone in source.Zones)
        {
            ParseZone(zone, buildStorages, collectableDrops, gates, lockboxes, ships, stations);
        }
    }
}
