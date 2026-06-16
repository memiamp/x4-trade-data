using System.ComponentModel;
using MPL.X4.TradeData.UI.Models;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a ship browser control for the application.
/// </summary>
internal partial class ShipBrowserControl : UserControl
{
    #region Declarations

    private readonly ListViewColumnSorter _columnSorter = new();

    private IEnumerable<ShipBrowserListItem> _items = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="ShipBrowserControl"/> class.
    /// </summary>
    public ShipBrowserControl()
    {
        InitializeComponent();
        Initialise();
    }

    #endregion

    #region Methods

    private void DoRefresh()
    {
        var hasItems = _items.Any();

        NoItemsLabel.Visible = !hasItems;
        AbandonedCheckBox.Visible = hasItems;
        CargoCheckBox.Visible = hasItems;
        ModificationCheckBox.Visible = hasItems;
        ShipListView.Visible = hasItems;
    }

    private static ListViewItem GenerateListViewItem(ShipBrowserListItem source)
    {
        var returnValue = new ListViewItem(source.Class);
        returnValue.SubItems.Add($"{source.Name} ({source.Ship.Code})");
        returnValue.SubItems.Add(source.OwnerAcronym);
        returnValue.SubItems.Add(source.Location);
        returnValue.SubItems.Add(source.Cargo);
        returnValue.SubItems.Add(source.Modifications);
        returnValue.SubItems.Add(source.X);
        returnValue.SubItems.Add(source.Y);
        returnValue.SubItems.Add(source.Z);

        return returnValue;
    }

    private void Initialise()
    {
        ShipListView.ListViewItemSorter = _columnSorter;

        AbandonedCheckBox.Checked = true;
        CargoCheckBox.Checked = false;
        ModificationCheckBox.Checked = false;

        // Event wireup
        Load += SpecialItemControl_Load;
        AbandonedCheckBox.CheckedChanged += AbandonedCheckBox_CheckedChanged;
        CargoCheckBox.CheckedChanged += CargoCheckBox_CheckedChanged;
        ModificationCheckBox.CheckedChanged += ModificationCheckBox_CheckedChanged;
        ShipListView.ColumnClick += ShipListView_ColumnClick;

        DoRefresh();
    }

    internal void LoadData()
    {
        IEnumerable<ListViewItem> items = [];

        if (_items is not null)
        {
            var sourceItems = _items;

            if (AbandonedCheckBox.Checked)
            {
                sourceItems = sourceItems.Where(x => x.CanBeCaptured);
            }

            if (CargoCheckBox.Checked)
            {
                sourceItems = sourceItems.Where(x => x.HasCargo);
            }

            if (ModificationCheckBox.Checked)
            {
                sourceItems = sourceItems.Where(x => x.HasModifications);
            }

            items = sourceItems
                               .OrderBy(x => x.Name)
                               .ThenBy(x => x.OwnerAcronym)
                               .Select(GenerateListViewItem);
        }

        ShipListView.BeginUpdate();

        ShipListView.Items.Clear();
        ShipListView.Items.AddRange([.. items]);

        ShipListView.EndUpdate();

        DoRefresh();
    }

    #endregion

    #region Event Handlers

    private void AbandonedCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void CargoCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void ModificationCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void ShipListView_ColumnClick(object? sender, ColumnClickEventArgs e)
    {
        if (e.Column == _columnSorter.SortColumn)
        {
            _columnSorter.SortOrder = _columnSorter.SortOrder == SortOrder.Ascending
                ? SortOrder.Descending
                : SortOrder.Ascending;
        }
        else
        {
            _columnSorter.SortColumn = e.Column;
            _columnSorter.SortOrder = SortOrder.Ascending;
        }

        ShipListView.Sort();
    }

    private void SpecialItemControl_Load(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the items for the control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IEnumerable<ShipBrowserListItem> Items
    {
        get
        {
            return _items;
        }

        set
        {
            _items = value;
            LoadData();
        }
    }

    #endregion
}
