namespace MPL.X4.GameResources.Data;

/// <summary>
/// A class that implements a dictionary of ship model resource data models.
/// </summary>
internal class ShipModelResourceDataDictionary : MacroNameResourceDataDictionary, IShipModelResourceDataDictionary
{
    public required IDictionaryCollection<string, string> Aliases { get; init; }
}
