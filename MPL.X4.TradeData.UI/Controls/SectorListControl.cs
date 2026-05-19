using System.ComponentModel;
using MPL.X4.SaveGame.Models;
using MPL.X4.TradeData.UI.Models;

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
        var displayNoItems = _items.Any() == false;

        SectorListView.Visible = !displayNoItems;
        ViewSectorButton.Visible = !displayNoItems;
        NoItemsLabel.Visible = displayNoItems;

        DoRefreshSectorItem();
    }

    private void DoRefreshSectorItem()
    {
        ViewSectorButton.Enabled = GetSelectedItem() is not null;
    }

    private static ListViewItem GenerateListViewItem(ISectorModel source)
    {
        var returnValue = new ListViewItem(source.Name);
        returnValue.SubItems.Add(source.Code);
        returnValue.SubItems.Add(source.Ships.Count(x => x.CanBeCaptured).ToString());
        returnValue.SubItems.Add(source.Lockboxes.Count.ToString());
        returnValue.SubItems.Add(source.Ships.Count.ToString());
        returnValue.SubItems.Add(source.Stations.Count.ToString());

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
        // Event wireup
        Load += SpecialItemControl_Load;
        SectorListView.SelectedIndexChanged += SectorListView_SelectedIndexChanged;
        ViewSectorButton.Click += ViewSectorButton_Click;

        DoRefresh();
    }

    private void LoadItems()
    {
        var orderedItems = _items
                                 .OrderBy(x => x.Name)
                                 .Select(GenerateListViewItem);

        SectorListView.BeginUpdate();

        SectorListView.Items.Clear();
        SectorListView.Items.AddRange([.. orderedItems]);

        SectorListView.EndUpdate();

        DoRefresh();
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

    private void SectorListView_SelectedIndexChanged(object? sender, EventArgs e)
    {
        DoRefreshSectorItem();
    }

    private void SpecialItemControl_Load(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    private void ViewSectorButton_Click(object? sender, EventArgs e)
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
            LoadItems();
        }
    }

    #endregion
}
