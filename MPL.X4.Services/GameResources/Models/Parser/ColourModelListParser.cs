using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

/// <summary>
/// A class that implements a parser to an <see cref="IColourModelList"/> from an <see cref="IColourResourceData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="modelParser">An <see cref="IModelParser"/> that is the model parser to use.</param>
internal class ColourModelListParser(
                                     ILogger<ColourModelListParser> logger,
                                     IModelParser modelParser)
    : ModelParserBase<IColourResourceData, IColourModelList>(logger),
      IModelParser<IColourResourceData, IColourModelList>
{
    private protected override IColourModelList OnParse(IColourResourceData source)
    {
        var returnValue = new ColourModelList();

        var mappedColours = source.Mappings
                                           .Join(
                                                 source.Colours,
                                                 x => x.Value.Reference,
                                                 x => x.Key,
                                                 (x, y) => new ColourModelSource { Id = x.Key, Colour = y.Value });

        var results = mappedColours.Select(x => modelParser.Parse<ColourModelSource, IColourModel>(x));

        returnValue.AddRange(results);

        return returnValue;
    }
}
