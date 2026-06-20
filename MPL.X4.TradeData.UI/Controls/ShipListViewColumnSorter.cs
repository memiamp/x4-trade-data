using MPL.X4.SaveGame.Models.Services;
using MPL.X4.TradeData.UI.Models;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a list view column sorter for a <see cref="ShipBrowserListItem"/>.
/// </summary>
internal class ShipListViewColumnSorter : ListViewColumnSorter<ShipBrowserListItem>
{
    private int CompareCargo(ShipBrowserListItem x, ShipBrowserListItem y)
        => CompareValue(x.Ship.Cargo.TotalAmount, y.Ship.Cargo.TotalAmount);

    private int CompareModifications(ShipBrowserListItem x, ShipBrowserListItem y)
    {
        var (basicX, enhancedX, exceptionalX) = x.Ship.GetModificationQualityCount();
        var (basicY, enhancedY, exceptionalY) = y.Ship.GetModificationQualityCount();

        var returnValue = CompareValue(exceptionalX, exceptionalY);
        if (returnValue == 0)
        {
            returnValue = CompareValue(enhancedX, enhancedY);
        }
        if (returnValue == 0)
        {
            returnValue = CompareValue(basicX, basicY);
        }

        return returnValue;
    }

    private protected override int OnCompare(ShipBrowserListItem x, ShipBrowserListItem y, ListViewItem itemX, ListViewItem itemY)
    {
        var returnValue = SortColumn switch
        {
            0 or 1 or 2 or 3 => CompareStringColumn(itemX, itemY),
            4 => CompareCargo(x, y),
            5 => CompareModifications(x, y),
            6 or 7 or 8 => CompareIntColumn(itemX, itemY),
            _ => 0
        };

        if (returnValue == 0)
        {
            returnValue = CompareValue(x.Name, y.Name);
        }

        return returnValue;
    }
}