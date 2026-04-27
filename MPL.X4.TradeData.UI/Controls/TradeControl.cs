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

    private readonly BindingList<Ware> _wares = [];

    private IEnumerable<TradeOffer> _items = [];
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
            var sellToText = string.Empty;

            PopulateTrades(TradesListViewSellTo, TradeType.Buy, orderAsecending: false);
            PopulateTrades(TradesListViewBuyFrom, TradeType.Sell);

            if (_selectedWare != null)
            {
                buyFromText = $"Average BUY FROM price: {(float)_selectedWare.AverageBuyPrice / 100:0.00}";
                sellToText = $"Average SELL TO price: {(float)_selectedWare.AverageSellPrice / 100:0.00}";
            }

            lblAverageBuyFromPrice.Text = buyFromText;
            lblAverageSellToPrice.Text = sellToText;
        }

        MainLayoutPanel.Visible = !displayNoTrades;
        NoItemsLabel.Visible = displayNoTrades;
    }

    private ListViewItem GenerateListViewItem(TradeOffer source)
    {
        var averageBuyPrice = (int)(_selectedWare?.AverageBuyPrice ?? 0);
        var averageSellPrice = (int)(_selectedWare?.AverageSellPrice ?? 0);

        var backColour = source.Type switch
        {
            TradeType.Buy => ColourHelper.GetAverageGradientColour(source.Price, averageBuyPrice, 0.5),
            TradeType.Sell => ColourHelper.GetAverageGradientColour(source.Price, averageSellPrice, -0.5),
            _ => SystemColors.Window
        };

        var price = $"{source.Price / 100F:0.00}";

        var returnValue = new ListViewItem(source.SectorName)
        {
            BackColor = backColour
        };
        returnValue.SubItems.Add(source.StationName);
        returnValue.SubItems.Add($"{source.Amount:#,##0}");
        returnValue.SubItems.Add(price);
        return returnValue;
    }

    private void Initialise()
    {
        WareComboBox.DataSource = _wares;
        WareComboBox.DisplayMember = nameof(Ware.Name);

        // Event wireup
        Load += TradeControl_Load;
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
                                TotalPrice = g.Sum(x => (long)x.Price * x.Amount)
                            });

        var buyFroms = _items
                             .Where(x => x.Type == TradeType.Sell)
                             .GroupBy(x => x.Ware)
                             .Select(g => new
                             {
                                 Name = g.Key,
                                 TotalBuyAmount = g.Sum(x => x.Amount),
                                 TotalPrice = g.Sum(x => (long)x.Price * x.Amount)
                             });

        var wares = buyFroms
                            .Join(
                                  sellTos,
                                  x => x.Name,
                                  x => x.Name,
                                  (x, y) => new Ware
                                  {
                                      AverageBuyPrice = x.TotalPrice / x.TotalBuyAmount,
                                      AverageSellPrice = y.TotalPrice / y.TotalSellAmount,
                                      Name = x.Name,
                                      TotalBuyAvailability = x.TotalBuyAmount,
                                      TotalSellAvailability = y.TotalSellAmount,
                                  })
                            .OrderBy(x => x.Name);

        // Check unjoined results
        var allBuyNames = buyFroms.Select(x => x.Name).Distinct();
        var allSellNames = sellTos.Select(x => x.Name).Distinct();

        if (wares.Any(x => !allBuyNames.Contains(x.Name) || !allSellNames.Contains(x.Name)))
        {
            Console.WriteLine("MISSING ITEM FROM ONE LIST");
        }
        var selectedWareName = _selectedWare?.Name;

        _wares.Clear();
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

        DoRefresh();
    }

    private void PopulateTrades(ListView target, TradeType tradeType, bool orderAsecending = true)
    {
        var selectedWareName = _selectedWare?.Name;

        target.BeginUpdate();
        target.Items.Clear();

        if (_selectedWare != null)
        {
            var items = _items
                              .Where(x => x.Ware == _selectedWare?.Name &&
                                          x.Type == tradeType &&
                                          x.Amount > 0);

            items = orderAsecending
                                    ? items.OrderBy(x => x.Price)
                                    : items.OrderByDescending(x => x.Price);

            target.Items.AddRange([.. items.Select(GenerateListViewItem)]);
        }

        target.EndUpdate();
    }

    #endregion

    #region Event Handlers

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
