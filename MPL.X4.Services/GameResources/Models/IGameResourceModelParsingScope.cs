namespace MPL.X4.GameResources.Models;

/// <summary>
/// An interface that defines a scope for parsing game resource models.
/// </summary>
internal interface IGameResourceModelParsingScope
{
    /// <summary>
    /// Looks up the value of the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">An <see cref="ITextResourceReference"/> to look up.</param>
    /// <returns>A <see cref="string"/> containing the result.</returns>
    /// <exception cref="ArgumentException">Thrown when the specified <paramref name="source"/> cannot be found.</exception>
    string Lookup(ITextResourceReference source);

    /// <summary>
    /// Gets or sets the colours.
    /// </summary>
    IColourModelList Colours { get; set; }

    /// <summary>
    /// Gets or sets the text resources.
    /// </summary>
    ITextResourceModelList TextResources { get; set; }
}
