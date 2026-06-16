using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IUniverseModel"/> from an <see cref="IUniverseData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class UniverseModelParser(
                                   ILogger<UniverseModelParser> logger,
                                   IModelParser modelParser,
                                   ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<IUniverseData, IUniverseModel>(logger)
{
    private protected override IUniverseModel OnParse(IUniverseData source)
    {
        logger.LogInformation("Parsing universe model");

        var highwayShips = ParseHighways(source.Galaxy.Clusters.SelectMany(x => x.Highways));
        var sectors = modelParser.Parse<IEnumerable<ISectorData>, ISectorModelList>(source.Galaxy.Clusters.SelectMany(x => x.Sectors));

        return new UniverseModel
        {
            HighwayShips = highwayShips,
            Sectors = sectors
        };
    }

    private void ParseHighway(IHighwayData source, ShipModelList ships)
    {
        parsingScope.CurrentOffset = ITransform3D.GetDefault();

        foreach (var item in source.Ships)
        {
            var model = modelParser.Parse<IShipData, IShipModel>(item);
            ships.Add(model);
        }
    }

    private ShipModelList ParseHighways(IEnumerable<IHighwayData> source)
    {
        var returnValue = new ShipModelList();

        foreach (var highway in source)
        {
            ParseHighway(highway, returnValue);
        }

        return returnValue;
    }
}
