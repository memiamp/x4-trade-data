using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IFactionModel"/> from an <see cref="IFactionData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="IGameResourceModelParsingScope"/> that is the parsing scope.</param>
internal class FactionModelParser(
                                  ILogger<FactionModelParser> logger,
                                  IGameResourceModelParsingScope parsingScope)
    : ModelParserBase<IFactionData, IFactionModel>(logger),
      IModelParser<IFactionData, IFactionModel>
{
    private protected override IFactionModel OnParse(IFactionData source)
    {
        var colour = parsingScope.Colours.GetValueOrDefault(source.ColourReference);

        var acronym = source.AcronymResource is null
                                                     ? string.Empty
                                                     : parsingScope.Lookup(source.AcronymResource);
        var name = source.NameResource is null
                                               ? string.Empty
                                               : parsingScope.Lookup(source.NameResource);
        
        return new FactionModel
        {
            Acronym = acronym,
            Colour = colour,
            Id = source.Id,
            Name = name
        };
    }
}
