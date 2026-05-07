using MPL.X4.Data;

namespace MPL.X4.Services.Data;

/// <summary>
/// A class that implements a data model of a collection of factions.
/// </summary>
internal class FactionsData : DictionaryCollection<string, IFactionData>, IFactionsData
{
}
