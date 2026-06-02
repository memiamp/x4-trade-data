using System.ComponentModel;
using MPL.X4.GameResources.Models;
using MPL.X4.TradeData.UI.Helpers;
using MPL.X4.TradeData.UI.Models.Trade;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a trade panel control for the application.
/// </summary>
internal partial class TradePanelControl : UserControl
{
    #region Declarations

    private readonly int SectorColumnWidth;
    private readonly int WareColumnWidth;

    private decimal _averagePrice = 0m;
    private TradeDisplayMode _displaymode;
    private IEnumerable<ITradeListItem> _items = [];
    private string? _sectorName;
    private IFactionModel? _sectorOwner;
    private IFactionModel? _stationOwner;
    private string? _wareName;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="TradePanelControl"/> control.
    /// </summary>
    public TradePanelControl()
    {
        InitializeComponent();

        SectorColumnWidth = TradesListView_Sector.Width;
        WareColumnWidth = TradesListView_Ware.Width;

        Initialise();
    }

    #endregion

    #region Methods

    private void DoRefresh()
    {
        TradesListView_Sector.Width = _sectorName is null ? SectorColumnWidth : 0;
        TradesListView_Ware.Width = _wareName is null ? WareColumnWidth : 0;

        if (_averagePrice > 0)
        {
            AveragePriceLabel.Text = $"Average price: {_averagePrice:0.00}";
        }
        else
        {
            AveragePriceLabel.Text = string.Empty;
        }
    }

    private ListViewItem GenerateListViewItem(ITradeListItem source)
    {
        var deviation = _displaymode == TradeDisplayMode.Buy ? 0.5 : -0.5;
        var price = source.Trade.Price / 100D;
        var backColour = _averagePrice == 0
                                            ? Constants.Colours.Unset
                                            : ColourHelper.GetAverageGradientColour(price, (double)_averagePrice, deviation);

        var returnValue = new ListViewItem(source.Trade.Name)
        {
            BackColor = backColour
        };
        returnValue.SubItems.Add(source.SectorName);
        returnValue.SubItems.Add(source.StationName);
        returnValue.SubItems.Add($"{source.Trade.Amount:#,##0}");
        returnValue.SubItems.Add($"{price:0.00}");

        if (Constants.Sectors.HyperloopSectors.Contains(source.SectorName))
        {
            returnValue.UseItemStyleForSubItems = false;
            returnValue.SubItems[1].Font = new Font(TradesListView.Font.FontFamily,
                                                    TradesListView.Font.Size,
                                                    FontStyle.Bold);

            for (int i = 1; i < returnValue.SubItems.Count; i++)
            {
                returnValue.SubItems[i].BackColor = returnValue.SubItems[0].BackColor;
            }
        }

        return returnValue;
    }

    private void Initialise()
    {
        // Event wireup
        Load += TradePanelControl_Load;

        DoRefresh();
    }

    private void LoadTradeOffers()
    {
        var items = _items;

        if (_sectorName is not null)
        {
            items = items.Where(x => x.SectorName == _sectorName);
        }

        if (_sectorOwner is not null)
        {
            items = items.Where(x => x.SectorFaction == _sectorOwner);
        }

        if (_stationOwner is not null)
        {
            items = items.Where(x => x.StationFaction == _stationOwner);
        }

        if (_wareName is not null)
        {
            var totalAmount = _items
                                    .Where(x => x.Trade.Name == _wareName)
                                    .Sum(x => (decimal)x.Trade.Amount);
            var totalValue = _items
                                   .Where(x => x.Trade.Name == _wareName)
                                   .Sum(x => (decimal)x.Trade.Price * x.Trade.Amount);

            _averagePrice = totalAmount > 0
                                            ? totalValue / totalAmount / 100
                                            : 0m;

            items = items.Where(x => x.Trade.Name == _wareName);
        }
        else
        {
            _averagePrice = 0;
        }

            items = _displaymode == TradeDisplayMode.Buy
                ? items.OrderBy(x => x.Trade.Name).ThenByDescending(x => x.Trade.Price)
                : items.OrderBy(x => x.Trade.Name).ThenBy(x => x.Trade.Price);

        TradesListView.BeginUpdate();

        var listItems = items.Select(GenerateListViewItem).ToArray();
        TradesListView.Items.Clear();
        TradesListView.Items.AddRange(listItems);

        TradesListView.EndUpdate();

        DoRefresh();
    }

    #endregion

    #region Event Handlers

    private void TradePanelControl_Load(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    #endregion

    #region Properties
    
    /// <summary>
    /// Gets or sets the display mode of the trades.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal TradeDisplayMode DisplayMode
    {
        get
        {
            return _displaymode;
        }

        set
        {
            _displaymode = value;
            LoadTradeOffers();
        }
    }

    /// <summary>
    /// Gets or sets the sector name to filter by.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal string? SectorName
    {
        get
        {
            return _sectorName;
        }

        set
        {
            _sectorName = value;
            LoadTradeOffers();
        }
    }

    /// <summary>
    /// Gets or sets the sector owner to filter by.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IFactionModel? SectorOwner
    {
        get
        {
            return _sectorOwner;
        }

        set
        {
            _sectorOwner = value;
            LoadTradeOffers();
        }
    }

    /// <summary>
    /// Gets or sets the station owner to filter by.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IFactionModel? StationOwner
    {
        get
        {
            return _stationOwner;
        }

        set
        {
            _stationOwner = value;
            LoadTradeOffers();
        }
    }

    /// <summary>
    /// Gets or sets the trades to be displayed in the control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IEnumerable<ITradeListItem> Trades
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

    /// <summary>
    /// Gets or sets the ware name to filter by.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal string? WareName
    {
        get
        {
            return _wareName;
        }

        set
        {
            _wareName = value;
            LoadTradeOffers();
        }
    }

    #endregion
}
