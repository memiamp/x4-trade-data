using System.ComponentModel;
using MPL.X4.TradeData.UI.Models;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a special item control for the application.
/// </summary>
internal partial class SpecialItemControl : UserControl
{
    #region Declarations

    private IEnumerable<SpecialItem> _items = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="SpecialItemControl"/> class.
    /// </summary>
    public SpecialItemControl()
    {
        InitializeComponent();
        Initialise();
    }

    #endregion

    #region Methods

    private void DoRefresh()
    {
        var displayNoItems = _items.Any() == false;

        SpecialItemListView.Visible = !displayNoItems;
        NoItemsLabel.Visible = displayNoItems;
    }

    private static ListViewItem GenerateListViewItem(SpecialItem source)
    {
        var returnValue = new ListViewItem(source.Type.ToString());
        returnValue.SubItems.Add(source.Description);
        returnValue.SubItems.Add(source.Code);
        returnValue.SubItems.Add(source.SectorName);
        returnValue.SubItems.Add(source.X);
        returnValue.SubItems.Add(source.Y);
        returnValue.SubItems.Add(source.Z);
        return returnValue;
    }

    private void Initialise()
    {
        // Event wireup
        Load += SpecialItemControl_Load;

        DoRefresh();
    }

    private void LoadSpecialItems()
    {
        var orderedItems = _items
                                 .OrderBy(x => x.SectorName)
                                 .ThenBy(x => x.Type)
                                 .ThenBy(x => x.Description)
                                 .Select(GenerateListViewItem);

        SpecialItemListView.BeginUpdate();

        SpecialItemListView.Items.Clear();
        SpecialItemListView.Items.AddRange([.. orderedItems]);

        SpecialItemListView.EndUpdate();
   
        DoRefresh();
    }

    #endregion

    #region Event Handlers

    private void SpecialItemControl_Load(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the special items to be displayed in the control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IEnumerable<SpecialItem> Items
    {
        get
        {
            return _items;
        }

        set
        {
            _items = value;
            LoadSpecialItems();
        }
    }

    #endregion
}
