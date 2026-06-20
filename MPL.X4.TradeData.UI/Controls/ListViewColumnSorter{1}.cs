using System.Collections;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a column sorter for a list view.
/// </summary>
/// <typeparam name="T">The type of the object in the list view tag.</typeparam>
internal abstract class ListViewColumnSorter<T> : IComparer
{
    /// <summary>
    /// Compares an integer column from the specified <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <param name="x">A <see cref="ListViewItem"/> that is the first item to compare.</param>
    /// <param name="y">A <see cref="ListViewItem"/> that is the second item to compare.</param>
    /// <returns>An <see cref="int"/> that is the result.</returns>
    private protected int CompareIntColumn(ListViewItem x, ListViewItem y)
    {
        if (int.TryParse(GetColumnValue(x), out var valueX) &&
            int.TryParse(GetColumnValue(y), out var valueY))
        {
            return CompareValue(valueX, valueY);
        }

        return 0;
    }

    /// <summary>
    /// Compares a string column from the specified <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <param name="x">A <see cref="ListViewItem"/> that is the first item to compare.</param>
    /// <param name="y">A <see cref="ListViewItem"/> that is the second item to compare.</param>
    /// <returns>An <see cref="int"/> that is the result.</returns>
    private protected int CompareStringColumn(ListViewItem x, ListViewItem y)
        => CompareValue(GetColumnValue(x), GetColumnValue(y));

    /// <summary>
    /// Compares the specified <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <param name="x">A <see cref="int"/> that is the first item to compare.</param>
    /// <param name="y">A <see cref="int"/> that is the second item to compare.</param>
    /// <returns>An <see cref="int"/> that is the result.</returns>
    private protected int CompareValue(int x, int y)
        => GetOrderedResult(x.CompareTo(y));

    /// <summary>
    /// Compares the specified <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <param name="x">A <see cref="string"/> that is the first item to compare.</param>
    /// <param name="y">A <see cref="string"/> that is the second item to compare.</param>
    /// <returns>An <see cref="int"/> that is the result.</returns>
    private protected int CompareValue(string x, string y)
        => GetOrderedResult(String.Compare(x, y, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Gets the value of the current sort column from the specified <paramref name="item"/>.
    /// </summary>
    /// <param name="item">A <see cref="ListViewItem"/> to get the sort column value from.</param>
    /// <returns>A <see cref="string"/> containing the result.</returns>
    private string GetColumnValue(ListViewItem item)
        => item.SubItems[SortColumn].Text;

    /// <summary>
    /// Gets a sort result ordered depending upon the sort order.
    /// </summary>
    /// <param name="result">An <see cref="int"/> that is the original sort result.</param>
    /// <returns>An <see cref="int"/> that is the ordered sort result.</returns>
    private protected int GetOrderedResult(int result)
        => SortOrder == SortOrder.Ascending
                                            ? result
                                            : -result;

    /// <summary>
    /// Overridden to get a comparison result using the specified parameters.
    /// </summary>
    /// <param name="x">A <typeparamref name="T"/> that is the first item to compare.</param>
    /// <param name="y">A <typeparamref name="T"/> that is the second item to compare.</param>
    /// <param name="itemX">A <see cref="ListViewItem"/> that is the first list view item to compare.</param>
    /// <param name="itemY">A <see cref="ListViewItem"/> that is the second list view item to compare.</param>
    /// <returns>An <see cref="int"/> that is the result.</returns>
    private protected abstract int OnCompare(T x, T y, ListViewItem itemX, ListViewItem itemY);

    /// <summary>
    /// Gets or sets the sort column.
    /// </summary>
    internal int SortColumn { get; set; } = 0;

    /// <summary>
    /// Gets or sets the sort order.
    /// </summary>
    internal SortOrder SortOrder { get; set; } = SortOrder.Ascending;

    int IComparer.Compare(object? x, object? y)
    {
        if (x is ListViewItem itemX && itemX.Tag is T tagX &&
            y is ListViewItem itemY && itemY.Tag is T tagY)
        {
            return OnCompare(tagX, tagY, itemX, itemY);
        }

        return 0;
    }
}
