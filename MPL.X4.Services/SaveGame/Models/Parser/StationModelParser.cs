using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IStationModel"/> from an <see cref="IStationData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class StationModelParser(
                                  ILogger<StationModelParser> logger,
                                  ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<IStationData, IStationModel>(logger)
{
    private protected override IStationModel OnParse(IStationData source)
    {
        var name = ParseName(source);
        var transform = source.Transform.Add(parsingScope.CurrentOffset);

        return new StationModel
        {
            Code = source.Code,
            IsKnown = source.IsKnown,
            Id = source.Id,
            IsUnderConstruction = source.State == Constants.XmlDataFile.AttributeValue.State.Construction,
            IsWreck = source.State == Constants.XmlDataFile.AttributeValue.State.Wreck,
            Name = name,
            Owner = source.Owner,
            Transform = transform,
        };
    }

    private string ParseName(IStationData source)
    {
        string returnValue;

        parsingScope.GameResources.Factions.TryGetValue(source.Owner, out var owner);
        var ownerName = owner?.Name ?? "Unknown";

        if (!string.IsNullOrWhiteSpace(source.Name))
        {
            returnValue = source.Name;
        }
        else if (source.NameResource is not null &&
                 parsingScope.GameResources.Text.TryGetValue(source.NameResource, out var value))
        {
            returnValue = value.Text;
        }
        else if (source.BaseNameResource is not null &&
                 parsingScope.GameResources.Text.TryGetValue(source.BaseNameResource, out value))
        {
            returnValue = value.Text;
        }
        else if (source.Productions?.Any() == true)
        {
            returnValue = $"{ownerName} ";

            if (source.Productions.Count() > 1)
            {
                returnValue += "Refined Goods Complex";
            }
            else if (parsingScope.GameResources.WareNames.TryGetValue(source.Productions.First(), out var ware))
            {
                returnValue += $"{ware.Name} Factory";
            }

            if (source.NameIndex > 0)
            {
                returnValue += $" {source.NameIndex}";
            }
        }
        else if (parsingScope.GameResources.LandmarkNames.TryGetValue(source.Macro, out var landmark))
        {
            returnValue = landmark.Name;
        }
        else if (source.State == Constants.XmlDataFile.AttributeValue.State.Construction)
        {
            returnValue = $"{ownerName} Factory (Under Construction)";
        }
        else
        {
            returnValue = $"{ownerName} Unknown Station {source.Code}";
        }

        return returnValue;
    }
}
