using System.Diagnostics.CodeAnalysis;
using MPL.X4.GameResources.Models;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a scope for parsing save game models.
/// </summary>
internal interface ISaveGameModelParsingScope
{
    /// <summary>
    /// Parses the faction name of the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">A <see cref="string"/> containing the source data to parse.</param>
    /// <param name="defaultResult">A <see cref="string"/> containing the result result to return if not found.</param>
    /// <returns>A <see cref="string"/> containing the result, or <paramref name="defaultResult"/> if not found.</returns>
    string ParseFactionName(string source, string defaultResult = "");

    /// <summary>
    /// Parses the ware name of the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">A <see cref="string"/> containing the source data to parse.</param>
    /// <param name="defaultResult">A <see cref="string"/> containing the result result to return if not found.</param>
    /// <returns>A <see cref="string"/> containing the result, or <paramref name="defaultResult"/> if not found.</returns>
    string ParseWareName(string source, string defaultResult = "");

    /// <summary>
    /// Tries to parse the ware name of the specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">A <see cref="string"/> containing the source data to parse.</param>
    /// <param name="wareName">A nullable <see cref="string"/> that will be set to the result, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating success.</returns>
    bool TryParseWareName(string source, [NotNullWhen(true)] out string? wareName);

    /// <summary>
    /// Gets or sets the current offset.
    /// </summary>
    ITransform3D CurrentOffset { get; set; }

    /// <summary>
    /// Gets or sets the game resources.
    /// </summary>
    IGameResourceModels GameResources { get; set; }
}