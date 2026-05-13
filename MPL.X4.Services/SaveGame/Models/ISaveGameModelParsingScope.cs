using MPL.X4.GameResources.Models;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a scope for parsing save game models.
/// </summary>
internal interface ISaveGameModelParsingScope
{
    /// <summary>
    /// Gets or sets the current offset.
    /// </summary>
    ITransform3D CurrentOffset { get; set; }

    /// <summary>
    /// Gets or sets the game resources.
    /// </summary>
    IGameResourceModels GameResources { get; set; }
}
