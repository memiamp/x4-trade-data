namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a paint modification data model.
/// </summary>
public interface IPaintModificationData : IModificationData
{
    /// <summary>
    /// Gets an indication of whether the paint is generated.
    /// </summary>
    bool Generated { get; }
}
