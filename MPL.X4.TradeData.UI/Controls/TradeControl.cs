using System.ComponentModel;
using MPL.X4.TradeData.UI.Helpers;
using MPL.X4.TradeData.UI.Models;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a trade control for the application.
/// </summary>
internal partial class TradeControl : UserControl
{
    #region Declarations

    private const string SectorOwnerAny = "Any";
    private const string StationOwnerAny = "Any";
    private const string WareAny = "Any";
    private const int WareColumnWidth = 100;

    private static readonly List<string> _loopSectors =
    [
        "Argon Prime",
        "Bright Promise",
        "Hatikvah's Choice I",
        "Holy Vision",
        "Pious Mists II",
        "Pontifex's Claim",
        "Profit Center Alpha",
        "Second Contact II Flashpoint",
        "Silent Witness I",
        "Trinity Sanctum III",
        "True Sight",
        "Unholy Retribution"
    ];

    private readonly BindingList<string> _sectorOwners = [];
    private readonly BindingList<string> _stationOwners = [];
    private readonly BindingList<Ware> _wares = [];

    private IEnumerable<TradeOffer> _items = [];
    private string? _selectedSectorOwner;
    private string? _selectedStationOwner;
    private Ware? _selectedWare;

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
        var displayNoTrades = _items.Any() == false;

        if (!displayNoTrades)
        {
            var buyFromText = string.Empty;
            var isWareSelected = !GetIsWareAnySelected();
            var sellToText = string.Empty;
            var wareColumnWidth = isWareSelected ? 0 : WareColumnWidth;

            PopulateTrades(TradesListViewSellTo, TradeType.Buy, orderAsecending: false);
            PopulateTrades(TradesListViewBuyFrom, TradeType.Sell);

            if (isWareSelected &&
                _selectedWare is not null)
            {
                buyFromText = $"Average BUY FROM price: {(float)_selectedWare.AverageBuyPrice / 100:0.00}";
                sellToText = $"Average SELL TO price: {(float)_selectedWare.AverageSellPrice / 100:0.00}";
            }

            AverageBuyFromPriceLabel.Text = buyFromText;
            AverageBuyFromPriceLabel.Visible = isWareSelected;
            AverageSellToPriceLabel.Text = sellToText;
            AverageSellToPriceLabel.Visible = isWareSelected;

            TradesListViewBuyFrom_Ware.Width = wareColumnWidth;
            TradesListViewSellTo_Ware.Width = wareColumnWidth;
        }

        MainLayoutPanel.Visible = !displayNoTrades;
        NoItemsLabel.Visible = displayNoTrades;
    }

    private ListViewItem GenerateListViewItem(TradeOffer source)
    {
        var averageBuyPrice = (int)(_selectedWare?.AverageBuyPrice ?? 0);
        var averageSellPrice = (int)(_selectedWare?.AverageSellPrice ?? 0);
        var isWareSelected = !GetIsWareAnySelected();

        var backColour = (isWareSelected, source.Type) switch
        {
            (true, TradeType.Buy) => ColourHelper.GetAverageGradientColour(source.Price, averageBuyPrice, 0.5),
            (true, TradeType.Sell) => ColourHelper.GetAverageGradientColour(source.Price, averageSellPrice, -0.5),
            _ => SystemColors.Window
        };

        var price = $"{source.Price / 100F:0.00}";

        var itemList = new List<string>
        {
            source.Ware,
            source.SectorName,
            source.StationName,
            $"{source.Amount:#,##0}",
            price
        };

        var returnValue = new ListViewItem(itemList[0])
        {
            BackColor = backColour,
        };
        foreach (var item in itemList.Skip(1))
        {
            returnValue.SubItems.Add(item);
        }

        if (_loopSectors.Contains(source.SectorName))
        {
            returnValue.UseItemStyleForSubItems = false;
            returnValue.SubItems[1].Font = new Font(TradesListViewBuyFrom.Font.FontFamily,
                                                               TradesListViewBuyFrom.Font.Size,
                                                               FontStyle.Bold);
            for (int i = 1; i < returnValue.SubItems.Count; i++)
            {
                returnValue.SubItems[i].BackColor = returnValue.SubItems[0].BackColor;
            }
        }

        return returnValue;
    }

    private bool GetIsWareAnySelected()
        => _selectedWare?.Name == WareAny;

    private void Initialise()
    {
        SectorOwnerComboBox.DataSource = _sectorOwners;
        StationOwnerComboBox.DataSource = _stationOwners;
        WareComboBox.DataSource = _wares;
        WareComboBox.DisplayMember = nameof(Ware.Name);

        // Event wireup
        Load += TradeControl_Load;
        SectorOwnerComboBox.SelectedValueChanged += SectorOwnerComboBox_SelectedValueChanged;
        StationOwnerComboBox.SelectedValueChanged += StationOwnerComboBox_SelectedValueChanged;
        WareComboBox.SelectedValueChanged += WareComboBox_SelectedValueChanged;

        DoRefresh();
    }

    private void LoadTradeOffers()
    {
        var sellTos = _items
                            .Where(x => x.Type == TradeType.Buy)
                            .GroupBy(x => x.Ware)
                            .Select(g => new
                            {
                                Name = g.Key,
                                TotalSellAmount = g.Sum(x => x.Amount),
                                TotalSellPrice = g.Sum(x => (long)x.Price * x.Amount)
                            });

        var buyFroms = _items
                             .Where(x => x.Type == TradeType.Sell)
                             .GroupBy(x => x.Ware)
                             .Select(g => new
                             {
                                 Name = g.Key,
                                 TotalBuyAmount = g.Sum(x => x.Amount),
                                 TotalBuyPrice = g.Sum(x => (long)x.Price * x.Amount)
                             });

        var wares = buyFroms
                            .GroupJoin(
                                       sellTos,
                                       b => b.Name,
                                       s => s.Name,
                                       (b, sellGroup) => new { Buy = b, Sell = sellGroup.DefaultIfEmpty() })
                            .SelectMany(
                                        x => x.Sell.Select(s => new Ware
                                        {
                                            Name = x.Buy.Name,
                                            TotalBuyAvailability = x.Buy.TotalBuyAmount,
                                            AverageBuyPrice = x.Buy.TotalBuyPrice / x.Buy.TotalBuyAmount,

                                            TotalSellAvailability = s?.TotalSellAmount ?? 0,
                                            AverageSellPrice = s != null && s.TotalSellAmount > 0
                                                                                                  ? s.TotalSellPrice / s.TotalSellAmount
                                                                                                  : 0
                                        }))
                            .Union(
                                   sellTos
                                          .GroupJoin(
                                                     buyFroms,
                                                     s => s.Name,
                                                     b => b.Name,
                                                     (s, buyGroup) => new { Sell = s, Buy = buyGroup.DefaultIfEmpty() })
                                          .SelectMany(
                                                      x => x.Buy.Select(b => new Ware
                                                      {
                                                          Name = x.Sell.Name,
                                                          TotalSellAvailability = x.Sell.TotalSellAmount,
                                                          AverageSellPrice = x.Sell.TotalSellPrice / x.Sell.TotalSellAmount,

                                                          TotalBuyAvailability = b?.TotalBuyAmount ?? 0,
                                                          AverageBuyPrice = b != null && b.TotalBuyAmount > 0
                                                                                                              ? b.TotalBuyPrice / b.TotalBuyAmount
                                                                                                              : 0
                                                      })))
                            .OrderBy(x => x.Name);

        var selectedWareName = _selectedWare?.Name;

        _wares.Clear();
        _wares.Add(new Ware
        {
            Name = WareAny
        });
        foreach (var ware in wares)
        {
            _wares.Add(ware);
        }

        if (!string.IsNullOrWhiteSpace(selectedWareName))
        {
            var newSelectedWare = _wares.FirstOrDefault(x => x.Name == selectedWareName);
            if (newSelectedWare != null)
            {
                WareComboBox.SelectedItem = newSelectedWare;
                DoRefresh();
            }
        }

        var sectorOwners = _items
                                 .Select(x => x.SectorOwner)
                                 .Distinct()
                                 .Order();

        _sectorOwners.Clear();
        _sectorOwners.Add(SectorOwnerAny);
        foreach (var sectorOwner in sectorOwners)
        {
            _sectorOwners.Add(sectorOwner);
        }

        var stationOwners = _items
                                  .Select(x => x.StationOwner)
                                  .Distinct()
                                  .Order();

        _stationOwners.Clear();
        _stationOwners.Add(StationOwnerAny);
        foreach (var stationOwner in stationOwners)
        {
            _stationOwners.Add(stationOwner);
        }

        DoRefresh();
    }

    private void PopulateTrades(ListView target, TradeType tradeType, bool orderAsecending = true)
    {
        IEnumerable<TradeOffer> items = [];
        var isWareSelected = !GetIsWareAnySelected();

        target.BeginUpdate();
        target.Items.Clear();

        items = isWareSelected
                               ? _items.Where(x => x.Ware == _selectedWare?.Name)
                               : _items;

        items = items.Where(x => (_selectedSectorOwner is null || x.SectorOwner == _selectedSectorOwner) &&
                                 (_selectedStationOwner is null || x.StationOwner == _selectedStationOwner) &&
                                 x.Type == tradeType &&
                                 x.Amount > 0);

        items = orderAsecending
                                ? items.OrderBy(x => x.Ware).ThenBy(x => x.Price)
                                : items.OrderBy(x => x.Ware).ThenByDescending(x => x.Price);

        target.Items.AddRange([.. items.Select(GenerateListViewItem)]);

        target.EndUpdate();
    }

    #endregion

    #region Event Handlers

    private void SectorOwnerComboBox_SelectedValueChanged(object? sender, EventArgs e)
    {
        if (SectorOwnerComboBox.SelectedValue is string value &&
            value != SectorOwnerAny)
        {
            _selectedSectorOwner = value;
        }
        else
        {
            _selectedSectorOwner = null;
        }

        DoRefresh();
    }

    private void StationOwnerComboBox_SelectedValueChanged(object? sender, EventArgs e)
    {
        if (StationOwnerComboBox.SelectedValue is string value &&
            value != StationOwnerAny)
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

    private void WareComboBox_SelectedValueChanged(object? sender, EventArgs e)
    {
        var newSelectedItem = WareComboBox.SelectedItem as Ware;
        if (newSelectedItem != _selectedWare)
        {
            _selectedWare = newSelectedItem;
            DoRefresh();
        }
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the trade offers to be displayed in the control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IEnumerable<TradeOffer> Items
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
