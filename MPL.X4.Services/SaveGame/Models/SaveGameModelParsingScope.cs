using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using MPL.X4.GameResources.Models;

namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a scope for parsing save game models.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class SaveGameModelParsingScope(
                                         ILogger<SaveGameModelParsingScope> logger)
    : ISaveGameModelParsingScope
{
    private ITransform3D _currentOffset = ITransform3D.GetDefault();

    ITransform3D ISaveGameModelParsingScope.CurrentOffset
    {
        get => _currentOffset;
        set => _currentOffset = value;
    }

    [AllowNull]
    IGameResourceModels ISaveGameModelParsingScope.GameResources { get; set; }
}
