using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="ILockboxModel"/> from an <see cref="ILockboxData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class LockboxModelParser(
                                 ILogger<LockboxModelParser> logger,
                                 ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<ILockboxData, ILockboxModel>(logger)
{
    private const string LockboxMacroIndex = "_0";
    private const string LockboxMacroIndicator = "lockbox_";
    private const char MacroSplitChar = '_';

    private static readonly Dictionary<string, LockboxRarity> _rarityMap = new()
    {
        { "big", LockboxRarity.Rare },
        { "common", LockboxRarity.Common },
        { "explosive", LockboxRarity.Rare },
        { "fragile", LockboxRarity.Rare },
        { "rare", LockboxRarity.Rare },
        { "special", LockboxRarity.Rare },
        { "trap", LockboxRarity.Rare },
        { "unusual", LockboxRarity.Unusual }
    };

    private protected override ILockboxModel OnParse(ILockboxData source)
    {
        var rarity = ParseRarity(source.Macro);
        var transform = source.Transform.Add(parsingScope.CurrentOffset);
        var wares = ParseWares(source.Wares);

        return new LockboxModel
        {
            Code = source.Code,
            IsKnown = source.IsKnown,
            Id = source.Id,
            LockCount = source.LockCount,
            Rarity = rarity,
            Transform = transform,
            Wares = wares
        };
    }

    private LockboxRarity ParseRarity(string macro)
    {
        var returnValue = LockboxRarity.Unknown;

        var lockboxPosition = macro.IndexOf(LockboxMacroIndicator);
        if (lockboxPosition >= 0)
        {
            var typeName = macro[(lockboxPosition + LockboxMacroIndicator.Length)..];

            var splitPosition = typeName.IndexOf(LockboxMacroIndex);
            if (splitPosition >= 0)
            {
                typeName = typeName[..splitPosition];

                if (!_rarityMap.TryGetValue(typeName, out returnValue) &&
                    typeName.Contains(MacroSplitChar))
                {
                    foreach (var part in typeName.Split(MacroSplitChar, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (_rarityMap.TryGetValue(part, out returnValue))
                        {
                            break;
                        }
                    }
                }
            }
        }

        if (returnValue == LockboxRarity.Unknown)
        {
            Logger.LogWarning("Unable to map lockbox macro {Macro} to a rarity", macro);
        }

        return returnValue;
    }

    private IEnumerable<string> ParseWares(IEnumerable<string> source)
    {
        var returnValue = new List<string>();

        foreach (var item in source)
        {
            if (parsingScope.TryParseWareName(item, out var ware))
            {
                returnValue.Add(ware);
            }
            else
            {
                returnValue.Add(item);
            }
        }

        return returnValue;
    }
}
