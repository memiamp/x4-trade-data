using System.Collections;

namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of a trade log.
/// </summary>
internal class TradeLogData : List<ITradeLogEntryData>, ITradeLogData
{
    public override string ToString()
        => $"{Count}";
}
