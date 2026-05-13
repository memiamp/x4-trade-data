using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IGateModel"/> from an <see cref="IGateData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class GateModelParser(
                               ILogger<GateModelParser> logger,
                               ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<IGateData, IGateModel>(logger)
{
    //private const string GateMacroIndex = "_0";
    //private const string GateMacroIndicator = "lockbox_";
    //private const char MacroSplitChar = '_';

    //private static readonly Dictionary<string, GateRarity> _rarityMap = new()
    //{
    //    { "big", GateRarity.Rare },
    //    { "common", GateRarity.Common },
    //    { "explosive", GateRarity.Rare },
    //    { "fragile", GateRarity.Rare },
    //    { "rare", GateRarity.Rare },
    //    { "special", GateRarity.Rare },
    //    { "trap", GateRarity.Rare },
    //    { "unusual", GateRarity.Unusual }
    //};

    private protected override IGateModel OnParse(IGateData source)
    {
        var type = ParseGateType(source.Macro);
        var transform = source.Transform.Add(parsingScope.CurrentOffset);

        return new GateModel
        {
            Code = source.Code,
            IsKnown = source.IsKnown,
            Id = source.Id,
            Transform = transform,
            Type = type
        };
    }

    private GateType ParseGateType(string macro)
    {
        var returnValue = GateType.Unknown;
        Console.WriteLine(macro);
        //var lockboxPosition = macro.IndexOf(GateMacroIndicator);
        //if (lockboxPosition >= 0)
        //{
        //    var typeName = macro[(lockboxPosition + GateMacroIndicator.Length)..];

        //    var splitPosition = typeName.IndexOf(GateMacroIndex);
        //    if (splitPosition >= 0)
        //    {
        //        typeName = typeName[..splitPosition];

        //        if (!_rarityMap.TryGetValue(typeName, out returnValue) &&
        //            typeName.Contains(MacroSplitChar))
        //        {
        //            foreach (var part in typeName.Split(MacroSplitChar, StringSplitOptions.RemoveEmptyEntries))
        //            {
        //                if (_rarityMap.TryGetValue(part, out returnValue))
        //                {
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        if (returnValue == GateType.Unknown)
        {
            Logger.LogWarning("Unable to map gate macro {Macro} to a type", macro);
        }

        return returnValue;
    }
}
