using System.ComponentModel;
using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Models;
using MPL.X4.TradeData.UI.Models;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a sector list control for the application.
/// </summary>
internal partial class SectorListControl : UserControl
{
    #region Declarations

    private readonly SectorListViewColumnSorter _columnSorter = new();

    private IEnumerable<SectorListItem> _items = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="SectorListControl"/> class.
    /// </summary>
    public SectorListControl()
    {
        InitializeComponent();
        Initialise();
    }

    #endregion

    #region Events

    /// <summary>
    /// An event that is raised when the user has requested to view a sector.
    /// </summary>
    public event EventHandler<SectorActionEventArgs>? ViewSectorRequest;

    #endregion

    #region Methods

    private void DoRefresh()
    {
        LoadItems();

        var hasItems = _items.Any();

        NoItemsLabel.Visible = !hasItems;

        AbandonedShipsCheckBox.Visible = hasItems;
        BuildStorageCheckBox.Visible = hasItems;
        DropsCheckBox.Visible = hasItems;
        LockboxesCheckBox.Visible = hasItems;
        ShipwreckCheckBox.Visible = hasItems;
        StationWreckCheckBox.Visible = hasItems;

        SectorOwnerComboBox.Visible = hasItems;
        SectorOwnerLabel.Visible = hasItems;

        SectorListView.Visible = hasItems;
        ViewSectorButton.Visible = hasItems;

        DoRefreshSectorItem();
    }

    private void DoRefreshSectorItem()
    {
        ViewSectorButton.Enabled = GetSelectedItem() is not null;
    }

    private static ListViewItem GenerateListViewItem(SectorListItem source)
    {
        var returnValue = new ListViewItem(source.Name)
        {
            BackColor = source.BackColour
        };

        returnValue.SubItems.Add(source.OwnerName);
        returnValue.SubItems.Add(source.AbandonedShipCount);
        returnValue.SubItems.Add(source.BuildStorageCount);
        returnValue.SubItems.Add(source.DropCount);
        returnValue.SubItems.Add(source.LockboxCount);
        returnValue.SubItems.Add(source.ShipCount);
        returnValue.SubItems.Add(source.ShipwreckCount);
        returnValue.SubItems.Add(source.StationCount);
        returnValue.SubItems.Add(source.StationWreckCount);

        returnValue.Tag = source;

        return returnValue;
    }

    private SectorListItem? GetSelectedItem()
        => SectorListView.SelectedItems is [{ Tag: SectorListItem returnValue }]
               ? returnValue
               : null;

    private void Initialise()
    {
        // Defaults
        AbandonedShipsCheckBox.Checked = false;
        BuildStorageCheckBox.Checked = false;
        DropsCheckBox.Checked = false;
        LockboxesCheckBox.Checked = false;
        ShipwreckCheckBox.Checked = false;
        StationWreckCheckBox.Checked = false;

        // Controls
        SectorListView.ListViewItemSorter = _columnSorter;
        SectorOwnerComboBox.DisplayMember = "Name";
        SectorOwnerComboBox.ValueMember = "Value";

        // Event wireup
        AbandonedShipsCheckBox.CheckedChanged += AbandonedShipsCheckBox_CheckedChanged;
        BuildStorageCheckBox.CheckedChanged += BuildStorageCheckBox_CheckedChanged;
        DropsCheckBox.CheckedChanged += DropsCheckBox_CheckedChanged;
        Load += SpecialItemControl_Load;
        LockboxesCheckBox.CheckedChanged += LockboxesCheckBox_CheckedChanged;
        SectorListView.ColumnClick += SectorListView_ColumnClick;
        SectorListView.DoubleClick += SectorListView_DoubleClick;
        SectorListView.SelectedIndexChanged += SectorListView_SelectedIndexChanged;
        SectorOwnerComboBox.SelectedIndexChanged += SectorOwnerComboBox_SelectedIndexChanged;
        ShipwreckCheckBox.CheckedChanged += ShipwreckCheckBox_CheckedChanged;
        StationWreckCheckBox.CheckedChanged += StationWreckCheckBox_CheckedChanged;
        ViewSectorButton.Click += ViewSectorButton_Click;

        DoRefresh();
    }

    private void LoadFactions()
    {
        var factions = _items
                             .Select(x => x.Owner)
                             .Distinct()
                             .Select(x => new
                             {
                                 Name = string.IsNullOrWhiteSpace(x?.Name)
                                                                           ? Constants.Owner.Unowned
                                                                           : x.Name,
                                 Value = x
                             })
                             .OrderBy(x => x.Name)
                             .Prepend(new
                             {
                                 Name = "Any",
                                 Value = (IFactionModel?)null
                             })
                             .ToList();

        SectorOwnerComboBox.DataSource = factions;
    }

    private void LoadItems()
    {
        var filteredItems = _items;

        if (SectorOwnerComboBox.SelectedValue is IFactionModel faction)
        {
            filteredItems = filteredItems.Where(x => x.Sector.Owner == faction);
        }

        if (AbandonedShipsCheckBox.Checked)
        {
            filteredItems = filteredItems.Where(x => x.AbandonedShipCount > 0);
        }

        if (BuildStorageCheckBox.Checked)
        {
            filteredItems = filteredItems.Where(x => x.BuildStorageCount > 0);
        }

        if (DropsCheckBox.Checked)
        {
            filteredItems = filteredItems.Where(x => x.DropCount > 0);
        }

        if (LockboxesCheckBox.Checked)
        {
            filteredItems = filteredItems.Where(x => x.LockboxCount > 0);
        }

        if (ShipwreckCheckBox.Checked)
        {
            filteredItems = filteredItems.Where(x => x.ShipwreckCount > 0);
        }

        if (StationWreckCheckBox.Checked)
        {
            filteredItems = filteredItems.Where(x => x.StationWreckCount > 0);
        }

        var orderedItems = filteredItems
                                        .OrderBy(x => x.Name)
                                        .Select(GenerateListViewItem);

        SectorListView.BeginUpdate();

        SectorListView.Items.Clear();
        SectorListView.Items.AddRange([.. orderedItems]);

        SectorListView.EndUpdate();
    }

    private void OnViewSectorRequest()
    {
        var sector = GetSelectedItem();
        if (sector is not null)
        {
            OnViewSectorRequest(sector.Sector);
        }
        else
        {
            DoRefreshSectorItem();
        }
    }

    private void OnViewSectorRequest(ISectorModel? sector)
    {
        if (sector is not null)
        {
            ViewSectorRequest?.Invoke(this, new SectorActionEventArgs(sector));
        }
    }

    #endregion

    #region Event Handlers

    private void AbandonedShipsCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    private void BuildStorageCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    private void DropsCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    private void LockboxesCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    private void SectorListView_ColumnClick(object? sender, ColumnClickEventArgs e)
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

        SectorListView.Sort();
    }

    private void SectorListView_DoubleClick(object? sender, EventArgs e)
    {
        OnViewSectorRequest();
    }

    private void SectorListView_SelectedIndexChanged(object? sender, EventArgs e)
    {
        DoRefreshSectorItem();
    }

    private void SpecialItemControl_Load(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    private void SectorOwnerComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    private void ShipwreckCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    private void StationWreckCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    private void ViewSectorButton_Click(object? sender, EventArgs e)
    {
        OnViewSectorRequest();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the items to be displayed in the control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IEnumerable<SectorListItem> Items
    {
        get => _items;
        set
        {
            _items = value;
            LoadFactions();
            DoRefresh();
        }
    }

    #endregion
}
