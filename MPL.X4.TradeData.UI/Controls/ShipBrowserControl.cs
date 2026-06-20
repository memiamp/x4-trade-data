using System.ComponentModel;
using MPL.X4.GameResources.Models;
using MPL.X4.TradeData.UI.Forms;
using MPL.X4.TradeData.UI.Models;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a ship browser control for the application.
/// </summary>
internal partial class ShipBrowserControl : UserControl
{
    #region Declarations

    private const string LocationAny = "Any";
    private const string OwnerAny = "Any";
    private const string WareAny = "Any";
    private const string ValueProperty = "Value";

    private readonly ShipListViewColumnSorter _columnSorter = new();
    private readonly int LocationColumnWidth;
    private readonly int OwnerColumnWidth; 

    private IEnumerable<ShipBrowserListItem> _items = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="ShipBrowserControl"/> class.
    /// </summary>
    public ShipBrowserControl()
    {
        InitializeComponent();

        LocationColumnWidth = ShipListView_Location.Width;
        OwnerColumnWidth = ShipListView_Owner.Width;

        Initialise();
    }

    #endregion

    #region Methods

    internal void DoFilterData()
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

            if (LocationNameComboBox.SelectedValue is string location &&
                location != LocationAny)
            {
                sourceItems = sourceItems.Where(x => x.Location == location);
            }

            if (ModificationCheckBox.Checked)
            {
                sourceItems = sourceItems.Where(x => x.HasModifications);
            }

            if (OwnerComboBox.SelectedValue is IFactionModel owner)
            {
                sourceItems = sourceItems.Where(x => x.Ship.Owner == owner);
            }

            if (WareComboBox.SelectedValue is string ware &&
                ware != WareAny)
            {
                sourceItems = sourceItems.Where(x => x.Ship.Cargo.Any(x => x.Name == ware));
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

    private void DoRefresh()
    {
        var hasItems = _items.Any();

        ShipListView_Location.Width = GetHasLocationName() ? 0 : LocationColumnWidth;
        ShipListView_Owner.Width = OwnerComboBox.SelectedValue is null ? OwnerColumnWidth : 0;

        NoItemsLabel.Visible = !hasItems;
        AbandonedCheckBox.Visible = hasItems;
        CargoCheckBox.Visible = hasItems;
        ModificationCheckBox.Visible = hasItems;
        ShipListView.Visible = hasItems;

        DoRefreshShipItem();
    }

    private void DoRefreshShipItem()
    {
        ViewShipButton.Enabled = GetSelectedItem() is not null;
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

        returnValue.Tag = source;

        return returnValue;
    }

    private bool GetHasLocationName()
        => LocationNameComboBox.SelectedValue is string location &&
           location != LocationAny;

    private ShipBrowserListItem? GetSelectedItem()
    {
        if (ShipListView.SelectedItems.Count == 1 &&
            ShipListView.SelectedItems[0].Tag is ShipBrowserListItem returnValue)
        {
            return returnValue;
        }

        return null;
    }

    private void Initialise()
    {
        ShipListView.ListViewItemSorter = _columnSorter;

        AbandonedCheckBox.Checked = true;
        CargoCheckBox.Checked = false;
        ModificationCheckBox.Checked = false;
        OwnerComboBox.DisplayMember = nameof(IFactionModel.Name);
        OwnerComboBox.ValueMember = ValueProperty;

        // Event wireup
        Load += SpecialItemControl_Load;
        AbandonedCheckBox.CheckedChanged += AbandonedCheckBox_CheckedChanged;
        CargoCheckBox.CheckedChanged += CargoCheckBox_CheckedChanged;
        LocationNameComboBox.SelectedValueChanged += LocationNameComboBox_SelectedValueChanged;
        ModificationCheckBox.CheckedChanged += ModificationCheckBox_CheckedChanged;
        OwnerComboBox.SelectedValueChanged += OwnerComboBox_SelectedValueChanged;
        ShipListView.ColumnClick += ShipListView_ColumnClick;
        ShipListView.DoubleClick += ShipListView_DoubleClick;
        ShipListView.SelectedIndexChanged += ShipListView_SelectedIndexChanged;
        ViewShipButton.Click += ViewShipButton_Click;
        WareComboBox.SelectedValueChanged += WareComboBox_SelectedValueChanged;

        DoRefresh();
    }

    private void LoadData()
    {
        LoadLocations();
        LoadOwners();
        LoadWares();
    }

    private void LoadLocations()
    {
        var data = _items
                         .Select(x => x.Location)
                         .Distinct()
                         .Order()
                         .Prepend(LocationAny)
                         .ToList();

        LocationNameComboBox.DataSource = data;
    }

    private void LoadOwners()
    {
        var data = _items
                          .Select(x => x.Ship.Owner)
                          .Distinct()
                          .Select(x => new
                          {
                              Name = string.IsNullOrWhiteSpace(x.Name) ? Constants.Owner.Unowned : x.Name,
                              Value = (IFactionModel?)x
                          })
                          .OrderBy(x => x.Name)
                          .Prepend(new
                          {
                              Name = OwnerAny,
                              Value = (IFactionModel?)null
                          })
                          .ToList();
        OwnerComboBox.DataSource = data;
    }

    private void LoadWares()
    {
        var data = _items
                         .SelectMany(x => x.Ship.Cargo)
                         .Select(x => x.Name)
                         .Distinct()
                         .OrderBy(x => x)
                         .Prepend(WareAny)
                         .ToList();
        WareComboBox.DataSource = data;
    }

    private void OnViewShip()
    {
        var selectedShip = GetSelectedItem();
        if (selectedShip is not null)
        {
            var dialog = new ShipViewerForm(selectedShip);
            dialog.ShowDialog();
        }
        else
        {
            DoRefreshShipItem();
        }
    }

    #endregion

    #region Event Handlers

    private void AbandonedCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        DoFilterData();
    }

    private void CargoCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        DoFilterData();
    }

    private void LocationNameComboBox_SelectedValueChanged(object? sender, EventArgs e)
    {
        DoFilterData();
    }

    private void ModificationCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        DoFilterData();
    }

    private void OwnerComboBox_SelectedValueChanged(object? sender, EventArgs e)
    {
        DoFilterData();
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

    private void ShipListView_DoubleClick(object? sender, EventArgs e)
    {
        OnViewShip();
    }

    private void ShipListView_SelectedIndexChanged(object? sender, EventArgs e)
    {
        DoRefreshShipItem();
    }

    private void SpecialItemControl_Load(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    private void ViewShipButton_Click(object? sender, EventArgs e)
    {
        OnViewShip();
    }

    private void WareComboBox_SelectedValueChanged(object? sender, EventArgs e)
    {
        DoFilterData();
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
