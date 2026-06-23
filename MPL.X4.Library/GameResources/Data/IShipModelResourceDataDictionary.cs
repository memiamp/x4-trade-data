namespace MPL.X4.GameResources.Data;

/// <summary>
/// An interface that defines the behaviour of a dictionary of ship model resources.
/// </summary>
public interface IShipModelResourceDataDictionary : IMacroNameResourceDataDictionary
{
    /// <summary>
    /// Gets macro aliases that are defined for this macro name resource data.
    /// </summary>
    IDictionaryCollection<string, string> Aliases { get; }
}
