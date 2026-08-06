using static System.Windows.Forms.ListViewItem;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements extension methods to a <see cref="ListViewSubItemCollection"/>.
/// </summary>
internal static class ListViewSubItemCollectionExtensions
{
    /// <summary>
    /// Adds a subitem to the collection with the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="target">A <see cref="ListViewSubItemCollection"/> that is the target to add to.</param>
    /// <param name="value">An <see cref="int"/> that is the value to be added.</param>
    /// <returns>A <see cref="ListViewSubItem"/> that is the new sub item.</returns>
    internal static ListViewSubItem Add(this ListViewSubItemCollection target, int value)
        => target.Add(value.ToString());
}
