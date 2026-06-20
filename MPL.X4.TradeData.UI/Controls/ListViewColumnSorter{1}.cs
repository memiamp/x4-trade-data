using System.Collections;

namespace MPL.X4.TradeData.UI.Models;

internal class ListViewColumnSorter : IComparer
{
    private int _sortColumn = 0;
    private SortOrder _sortOrder = SortOrder.Ascending;

    public int SortColumn
    {
        get => _sortColumn;
        set => _sortColumn = value;
    }

    public SortOrder SortOrder
    {
        get => _sortOrder;
        set => _sortOrder = value;
    }

    int IComparer.Compare(object? x, object? y)
    {
        if (x is ListViewItem itemX &&
            y is ListViewItem itemY)
        {
            var valueX = itemX.SubItems[_sortColumn].Text;
            var valueY = itemY.SubItems[_sortColumn].Text;

            var result = String.Compare(valueX, valueY, StringComparison.OrdinalIgnoreCase);

            return _sortOrder == SortOrder.Ascending
                                                     ? result
                                                     : -result;
        }

        return 0;
    }
}
