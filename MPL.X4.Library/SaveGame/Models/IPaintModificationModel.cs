namespace MPL.X4.SaveGame.Models;

/// <summary>
/// An interface that defines a model of a paint modification.
/// </summary>
public interface IPaintModificationModel : IModificationModel
{
    /// <summary>
    /// Gets an indication of whether the paint modification was generated.
    /// </summary>
    bool IsGenerated { get; }
}
