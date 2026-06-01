using System.Drawing;
using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Data;
using MPL.X4.Parser;

namespace MPL.X4.GameResources.Models.Parser;

public record struct ColourModelSource(string Id, IColourData Colour);

/// <summary>
/// A class that implements a parser to a <see cref="IColourModel"/> from an <see cref="IColourData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ColourModelParser(
                                 ILogger<ColourModelParser> logger)
    : ModelParserBase<ColourModelSource, IColourModel>(logger)
{
    private protected override IColourModel OnParse(ColourModelSource source)
    {
        var colour = Color.FromArgb(
                                    source.Colour.Alpha,
                                    source.Colour.Red,
                                    source.Colour.Green,
                                    source.Colour.Blue);

        return new ColourModel
        {
            Colour = colour,
            Glow = source.Colour.Glow,
            Id = source.Id
        };
    }
}
