using System.ComponentModel;
using MPL.X4.GameResources.Models;
using MPL.X4.SaveGame.Models;
using MPL.X4.TradeData.UI.Models.Trade;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a trade control for the application.
/// </summary>
internal partial class TradeControl : UserControl
{
    #region Declarations

    private const string SectorOwnerAny = "Any";
    private const string StationOwnerAny = "Any";
    private const string ValueProperty = "Value";
    private const string WareAny = "Any";

    private IEnumerable<ITradeListItem> _items = [];
    private IFactionModel? _selectedSectorOwner;
    private IFactionModel? _selectedStationOwner;
    private string? _selectedWare;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="TradeControl"/> class.
    /// </summary>
    public TradeControl()
    {
        InitializeComponent();
        Initialise();
    }

    #endregion

    #region Methods

    private void DoRefresh()
    {
        BuyTradePanel.SectorOwner = _selectedSectorOwner;
        BuyTradePanel.StationOwner = _selectedStationOwner;
        BuyTradePanel.WareName = _selectedWare == WareAny ? null : _selectedWare;

        SellTradePanel.SectorOwner = _selectedSectorOwner;
        SellTradePanel.StationOwner = _selectedStationOwner;
        SellTradePanel.WareName = _selectedWare == WareAny ? null : _selectedWare;

        var displayNoTrades = _items.Any() == false;
        MainLayoutPanel.Visible = !displayNoTrades;
        NoItemsLabel.Visible = displayNoTrades;
        TopPanel.Visible = !displayNoTrades;
    }

    private void Initialise()
    {
        BuyTradePanel.DisplayMode = TradeDisplayMode.Buy;

        SectorOwnerComboBox.DisplayMember = nameof(IFactionModel.Name);
        SectorOwnerComboBox.ValueMember = ValueProperty;

        SellTradePanel.DisplayMode = TradeDisplayMode.Sell;

        StationOwnerComboBox.DisplayMember = nameof(IFactionModel.Name);
        StationOwnerComboBox.ValueMember = ValueProperty;

        WareComboBox.DisplayMember = nameof(ITradeModel.Name);
        WareComboBox.ValueMember = ValueProperty;

        // Event wireup
        Load += TradeControl_Load;
        SectorOwnerComboBox.SelectedIndexChanged += SectorOwnerComboBox_SelectedIndexChanged;
        StationOwnerComboBox.SelectedIndexChanged += StationOwnerComboBox_SelectedIndexChanged;
        WareComboBox.SelectedIndexChanged += WareComboBox_SelectedIndexChanged;

        DoRefresh();
    }

    private void LoadFactionDataSource(ComboBox target, string anyText, Func<ITradeListItem, IFactionModel> selector)
    {
        var data = _items
                          .Select(selector)
                          .Distinct()
                          .Select(x => new
                          {
                              Name = string.IsNullOrWhiteSpace(x.Name) ? Constants.Owner.Unowned : x.Name,
                              Value = (IFactionModel?)x
                          })
                          .OrderBy(x => x.Name)
                          .Prepend(new
                          {
                              Name = anyText,
                              Value = (IFactionModel?)null
                          })
                          .ToList();
        target.DataSource = data;
    }

    private void LoadSectorOwners()
        => LoadFactionDataSource(SectorOwnerComboBox, SectorOwnerAny, x => x.SectorFaction);

    private void LoadStationOwners()
        => LoadFactionDataSource(StationOwnerComboBox, StationOwnerAny, x => x.StationFaction);

    private void LoadTradeOffers()
    {
        BuyTradePanel.Trades = _items.Where(x => x.Trade.Type == TradeType.Buy);
        SellTradePanel.Trades = _items.Where(x => x.Trade.Type == TradeType.Sell);

        LoadSectorOwners();
        LoadStationOwners();
        LoadWares();

        DoRefresh();
    }

    private void LoadWares()
    {
        var wares = _items
                          .Select(x => x.Trade.Name)
                          .Distinct()
                          .Select(x => new
                          {
                              Name = x,
                              Value = (string?)x
                          })
                          .OrderBy(x => x.Name)
                          .Prepend(new
                          {
                              Name = WareAny,
                              Value = (string?)null
                          })
                          .ToList();
        WareComboBox.DataSource = wares;
    }

    #endregion

    #region Event Handlers

    private void SectorOwnerComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (SectorOwnerComboBox.SelectedValue is IFactionModel value)
        {
            _selectedSectorOwner = value;
        }
        else
        {
            _selectedSectorOwner = null;
        }

        DoRefresh();
    }

    private void StationOwnerComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (StationOwnerComboBox.SelectedValue is IFactionModel value)
        {
            _selectedStationOwner = value;
        }
        else
        {
            _selectedStationOwner = null;
        }

        DoRefresh();
    }

    private void TradeControl_Load(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    private void WareComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (WareComboBox.SelectedValue is string value)
        {
            _selectedWare = value;
        }
        else
        {
            _selectedWare = null;
        }

        DoRefresh();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the trade offers to be displayed in the control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IEnumerable<ITradeListItem> Items
    {
        get
        {
            return _items;
        }

        set
        {
            _items = value;
            LoadTradeOffers();
        }
    }

    #endregion
}
