using MPL.X4.TradeData.UI.Models;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a list view column sorter for a <see cref="SectorListItem"/>.
/// </summary>
internal class SectorListViewColumnSorter : ListViewColumnSorter<SectorListItem>
{
    private protected override int OnCompare(SectorListItem x, SectorListItem y, ListViewItem itemX, ListViewItem itemY)
    {
        var returnValue = SortColumn switch
        {
            0 or 1 => CompareStringColumn(itemX, itemY),
            >= 2 and <= 9 => CompareIntColumn(itemX, itemY),
            _ => 0
        };

        if (returnValue == 0)
        {
            returnValue = CompareValue(x.Name, y.Name);
        }

        return returnValue;
    }
}