using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;
using MPL.X4.Services;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IModificationModel"/> from an <see cref="IModificationData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class ModificationModelParser(
                                       ILogger<ModificationModelParser> logger,
                                       ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<IModificationData, IModificationModel>(logger)
{
    private protected override IModificationModel OnParse(IModificationData source)
    {
        var name = parsingScope.ParseWareName(source.Ware);
        var summary = ParseSummary(source);
        var type = ParseType(source);

        return new ModificationModel
        {
            Id = source.Ware,
            Name = name,
            Summary = summary,
            Type = type
        };
    }

    private static string ParseSummary(IEngineModificationData source)
        => DebugOutputHelper.GetPropertyValues<double>(source, x => x != 0);

    private static string ParseSummary(IModificationData source)
        => source switch
        {
            IEngineModificationData x => ParseSummary(x),
            IPaintModificationData x => ParseSummary(x),
            IShieldModificationData x => ParseSummary(x),
            IShipModificationData x => ParseSummary(x),
            IWeaponModificationData x => ParseSummary(x),
            _ => string.Empty
        };

    private static string ParseSummary(IPaintModificationData source)
        => string.Empty;

    private static string ParseSummary(IShieldModificationData source)
        => DebugOutputHelper.GetPropertyValues<double>(source, x => x != 0);

    private static string ParseSummary(IShipModificationData source)
    {
        var returnValue = DebugOutputHelper.GetPropertyValues<double>(source, x => x != 0);
        returnValue += DebugOutputHelper.GetPropertyValues<int>(source, x => x != 0);

        return returnValue;
    }

    private static string ParseSummary(IWeaponModificationData source)
        => DebugOutputHelper.GetPropertyValues<double>(source, x => x != 0);

    private static ModificationType ParseType(IModificationData source)
        => source switch
        {
            IEngineModificationData _ => ModificationType.Engine,
            IPaintModificationData _ => ModificationType.Paint,
            IShieldModificationData _ => ModificationType.Shield,
            IShipModificationData _ => ModificationType.Ship,
            IWeaponModificationData _ => ModificationType.Weapon,
            _ => ModificationType.Unknown
        };
}
