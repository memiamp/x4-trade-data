namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines an offset data model.
/// </summary>
public interface IOffsetData
{
    /// <summary>
    /// Gets the offset macro.
    /// </summary>
    string Macro { get; }

    /// <summary>
    /// Gets the name of the offset.
    /// </summary>
    string Name{ get; }

    /// <summary>
    /// Gets the offset.
    /// </summary>
    ITransform3D Offset { get; }

    /// <summary>
    /// Gets the reference type of the offset.
    /// </summary>
    string ReferenceType { get; }
}
