namespace MPL.X4.SaveGame.Data;

/// <summary>
/// An interface that defines a data model of an item of ammunition.
/// </summary>
public interface IAmmunitionItemData
{
    /// <summary>
    /// Gets the ammunition amount.
    /// </summary>
    int Amount { get; }

    /// <summary>
    /// Gets the exact ammunition amount.
    /// </summary>
    int Exact { get; }

    /// <summary>
    /// Gets the ammunition macro.
    /// </summary>
    string Macro { get; }
}
