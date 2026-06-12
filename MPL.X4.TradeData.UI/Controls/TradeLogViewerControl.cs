using System.ComponentModel;
using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a sector list control for the application.
/// </summary>
internal partial class TradeLogViewerControl : UserControl
{
    #region Declarations

    private IEnumerable<ITradeLogEntryModel> _items = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="TradeLogViewerControl"/> class.
    /// </summary>
    public TradeLogViewerControl()
    {
        InitializeComponent();
        Initialise();
    }

    #endregion

    #region Methods

    private void DoRefresh()
    {
        var displayNoItems = _items.Any() == false;

        TradeLogListView.Visible = !displayNoItems;
        NoItemsLabel.Visible = displayNoItems;
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
        if (TradeLogListView.SelectedItems.Count == 1 &&
            TradeLogListView.SelectedItems[0].Tag is ISectorModel returnValue)
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
        //AbandonedShipsCheckBox.CheckedChanged += AbandonedShipsCheckBox_CheckedChanged;
        //BuildStorageCheckBox.CheckedChanged += BuildStorageCheckBox_CheckedChanged;
        //DropsCheckBox.CheckedChanged += DropsCheckBox_CheckedChanged;
        Load += TradeLogViewerControl_Load;
        //LockboxesCheckBox.CheckedChanged += LockboxesCheckBox_CheckedChanged;
        //TradeLogListView.DoubleClick += SectorListView_DoubleClick;
        //TradeLogListView.SelectedIndexChanged += SectorListView_SelectedIndexChanged;
        //SectorOwnerComboBox.SelectedIndexChanged += SectorOwnerComboBox_SelectedIndexChanged;
        //ViewSectorButton.Click += ViewSectorButton_Click;

        DoRefresh();
    }

    private void LoadItems()
    {
        //var filteredItems = _items;

        //if (SectorOwnerComboBox.SelectedValue is IFactionModel faction)
        //{
        //    filteredItems = filteredItems.Where(x => x.Owner == faction);
        //}

        //if (AbandonedShipsCheckBox.Checked)
        //{
        //    filteredItems = filteredItems.Where(x => x.Ships.Any(x => x.CanBeCaptured));
        //}

        //if (BuildStorageCheckBox.Checked)
        //{
        //    filteredItems = filteredItems.Where(x => x.BuildStorages.Count > 0);
        //}

        //if (DropsCheckBox.Checked)
        //{
        //    filteredItems = filteredItems.Where(x => x.CollectableDrops.Count > 0);
        //}

        //if (LockboxesCheckBox.Checked)
        //{
        //    filteredItems = filteredItems.Where(x => x.Lockboxes.Count > 0);
        //}

        //var orderedItems = _items
        //                         .OrderBy(x => x.Name)
        //                         .Select(GenerateListViewItem);

        TradeLogListView.BeginUpdate();

        TradeLogListView.Items.Clear();
        //TradeLogListView.Items.AddRange([.. orderedItems]);

        TradeLogListView.EndUpdate();

        DoRefresh();
    }

    #endregion

    #region Event Handlers

    //private void AbandonedShipsCheckBox_CheckedChanged(object? sender, EventArgs e)
    //{
    //    DoRefresh();
    //}

    //private void BuildStorageCheckBox_CheckedChanged(object? sender, EventArgs e)
    //{
    //    DoRefresh();
    //}

    //private void DropsCheckBox_CheckedChanged(object? sender, EventArgs e)
    //{
    //    DoRefresh();
    //}

    //private void LockboxesCheckBox_CheckedChanged(object? sender, EventArgs e)
    //{
    //    DoRefresh();
    //}

    //private void SectorListView_DoubleClick(object? sender, EventArgs e)
    //{
    //    OnViewSectorRequest();
    //}

    //private void SectorListView_SelectedIndexChanged(object? sender, EventArgs e)
    //{
    //    DoRefreshSectorItem();
    //}

    private void TradeLogViewerControl_Load(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    //private void SectorOwnerComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    //{
    //    DoRefresh();
    //}

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the items to be displayed in the control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IEnumerable<ITradeLogEntryModel> TradeLog
    {
        get
        {
            return _items;
        }

        set
        {
            _items = value;
            LoadItems();
        }
    }

    #endregion
}
