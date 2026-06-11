namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of a removed object.
/// </summary>
public interface IRemovedObjectData
{
    /// <summary>
    /// Gets the code of the removed object.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the identifier of the removed object.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the name of the removed object.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the owner of the removed object.
    /// </summary>
    string Owner { get; }

    /// <summary>
    /// Gets the space the removed object belonged to.
    /// </summary>
    string Space { get; }
}
