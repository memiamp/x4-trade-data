using System.ComponentModel;
using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a trade log viewer control for the application.
/// </summary>
internal partial class TradeLogViewerControl : UserControl
{
    #region Declarations

    private const string NameProperty = "Name";
    private const string TradePartnerAny = "Any";
    private const string ValueProperty = "Value";

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

    private void DoFilterData()
    {
        var filteredItems = _items;

        if (TradePartnerComboBox.SelectedValue is ITradePartnerModel model)
        {
            filteredItems = filteredItems.Where(x => x.Buyer == model || x.Seller == model);
        }

        var orderedItems = filteredItems
                                        .OrderBy(x => x.TradeAge)
                                        .Select(GenerateListViewItem);

        TradeLogListView.BeginUpdate();

        TradeLogListView.Items.Clear();
        TradeLogListView.Items.AddRange([.. orderedItems]);

        TradeLogListView.EndUpdate();

        DoRefresh();
    }

    private void DoRefresh()
    {
        var hasItems = _items.Any();

        NoItemsLabel.Visible = !hasItems;

        TradeLogListView.Visible = hasItems;

        TradePartnerComboBox.Visible = hasItems;
        TradePartnerLabel.Visible = hasItems;
    }

    private static ListViewItem GenerateListViewItem(ITradeLogEntryModel source)
    {
        var returnValue = new ListViewItem(source.Buyer.Name);

        returnValue.SubItems.Add(source.Buyer.Type.ToString());
        returnValue.SubItems.Add(source.Seller.Name);
        returnValue.SubItems.Add(source.Seller.Type.ToString());
        returnValue.SubItems.Add(source.Name);
        returnValue.SubItems.Add(source.Price.ToString());
        returnValue.SubItems.Add(source.TradeAge.ToString());

        returnValue.Tag = source;

        return returnValue;
    }

    private ITradeLogEntryModel? GetSelectedItem()
    {
        if (TradeLogListView.SelectedItems.Count == 1 &&
            TradeLogListView.SelectedItems[0].Tag is ITradeLogEntryModel returnValue)
        {
            return returnValue;
        }

        return null;
    }

    private void Initialise()
    {
        // Controls
        TradePartnerComboBox.DisplayMember = NameProperty;
        TradePartnerComboBox.ValueMember = ValueProperty;

        // Event wireup
        Load += TradeLogViewerControl_Load;
        TradePartnerComboBox.SelectedValueChanged += TradePartnerComboBox_SelectedValueChanged;

        DoRefresh();
    }

    private void LoadData()
    {
        LoadTradePartners();
    }

    private void LoadTradePartners()
    {
        var data = _items
                          .Select(x => x.Buyer)
                          .Concat(_items.Select(x => x.Seller))
                          .Distinct()
                          .Select(x => new
                          {
                              Name = $"{x.Name} ({x.Type})",
                              Value = (ITradePartnerModel?)x
                          })
                          .OrderBy(x => x.Name)
                          .Prepend(new
                          {
                              Name = TradePartnerAny,
                              Value = (ITradePartnerModel?)null
                          })
                          .ToList();

        TradePartnerComboBox.DataSource = data;
    }

    #endregion

    #region Event Handlers

    private void TradeLogViewerControl_Load(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    private void TradePartnerComboBox_SelectedValueChanged(object? sender, EventArgs e)
    {
        DoFilterData();
    }

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
            LoadData();
        }
    }

    #endregion
}
