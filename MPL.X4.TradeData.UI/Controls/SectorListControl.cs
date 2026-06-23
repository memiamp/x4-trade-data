using System.ComponentModel;
using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a sector list control for the application.
/// </summary>
internal partial class SectorListControl : UserControl
{
    #region Declarations

    private IEnumerable<ISectorModel> _items = [];

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

    private static ListViewItem GenerateListViewItem(ISectorModel source)
    {
        var backColor = source.Owner?.Colour is not null
                                                         ? source.Owner.Colour.Colour
                                                         : Constants.Colours.Unset;

        var owner = string.IsNullOrWhiteSpace(source.Owner?.Name)
                                                                  ? Constants.Owner.Unowned
                                                                  : source.Owner.Name;

        var returnValue = new ListViewItem(source.Name)
        {
            BackColor = backColor
        };

        returnValue.SubItems.Add(owner);
        returnValue.SubItems.Add(source.Ships.Count(x => x.CanBeCaptured).ToString());
        returnValue.SubItems.Add(source.Lockboxes.Count.ToString());
        returnValue.SubItems.Add(source.Ships.Count.ToString());
        returnValue.SubItems.Add(source.Stations.Count.ToString());
        returnValue.SubItems.Add(source.CollectableDrops.Count.ToString());
        returnValue.SubItems.Add(source.BuildStorages.Count.ToString());

        returnValue.Tag = source;

        return returnValue;
    }

    private ISectorModel? GetSelectedItem()
    {
        if (SectorListView.SelectedItems.Count == 1 &&
            SectorListView.SelectedItems[0].Tag is ISectorModel returnValue)
        {
            return returnValue;
        }

        return null;
    }

    private void Initialise()
    {
        // Defaults
        AbandonedShipsCheckBox.Checked = false;
        BuildStorageCheckBox.Checked = false;
        DropsCheckBox.Checked = false;
        LockboxesCheckBox.Checked = false;

        // Controls
        SectorOwnerComboBox.DisplayMember = "Name";
        SectorOwnerComboBox.ValueMember = "Value";

        // Event wireup
        AbandonedShipsCheckBox.CheckedChanged += AbandonedShipsCheckBox_CheckedChanged;
        BuildStorageCheckBox.CheckedChanged += BuildStorageCheckBox_CheckedChanged;
        DropsCheckBox.CheckedChanged += DropsCheckBox_CheckedChanged;
        Load += SpecialItemControl_Load;
        LockboxesCheckBox.CheckedChanged += LockboxesCheckBox_CheckedChanged;
        SectorListView.DoubleClick += SectorListView_DoubleClick;
        SectorListView.SelectedIndexChanged += SectorListView_SelectedIndexChanged;
        SectorOwnerComboBox.SelectedIndexChanged += SectorOwnerComboBox_SelectedIndexChanged;
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
                                 Name = string.IsNullOrWhiteSpace(x.Name)
                                                                          ? Constants.Owner.Unowned
                                                                          : x.Name,
                                 Value = (IFactionModel?)x
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
            filteredItems = filteredItems.Where(x => x.Owner == faction);
        }

        if (AbandonedShipsCheckBox.Checked)
        {
            filteredItems = filteredItems.Where(x => x.Ships.Any(x => x.CanBeCaptured));
        }

        if (BuildStorageCheckBox.Checked)
        {
            filteredItems = filteredItems.Where(x => x.BuildStorages.Count > 0);
        }

        if (DropsCheckBox.Checked)
        {
            filteredItems = filteredItems.Where(x => x.CollectableDrops.Count > 0);
        }

        if (LockboxesCheckBox.Checked)
        {
            filteredItems = filteredItems.Where(x => x.Lockboxes.Count > 0);
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
            OnViewSectorRequest(sector);
        }
        else
        {
            DoRefreshSectorItem();
        }
    }

    private void OnViewSectorRequest(ISectorModel sector)
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
    internal IEnumerable<ISectorModel> Items
    {
        get
        {
            return _items;
        }

        set
        {
            _items = value;
            LoadFactions();
            DoRefresh();
        }
    }

    #endregion
}
