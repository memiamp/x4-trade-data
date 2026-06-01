using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IMacroNameModel"/> from an <see cref="IMacroNameResourceData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="IGameResourceModelParsingScope"/> that is the parsing scope.</param>
internal class MacroNameModelParser(
                                    ILogger<MacroNameModelParser> logger,
                                    IGameResourceModelParsingScope parsingScope)
    : ModelParserBase<IMacroNameResourceData, IMacroNameModel>(logger)
{
    private protected override IMacroNameModel OnParse(IMacroNameResourceData source)
        => new MacroNameModel
        {
            Id = source.Id,
            Name = parsingScope.Lookup(source.NameResource)
        };
}
