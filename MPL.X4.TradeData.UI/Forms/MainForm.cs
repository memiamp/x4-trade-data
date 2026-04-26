using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using MPL.X4.Services;
using MPL.X4.TradeData.Services;
using MPL.X4.TradeData.UI.Models;

namespace MPL.X4.TradeData.UI.Forms;

/// <summary>
/// A class that implements the main form for the application.
/// </summary>
internal partial class MainForm : Form
{
    private readonly List<AbandonedShip> _abandonedShips = [];
    private readonly DebugForm _debugForm;
    private readonly IResourceDataLoader _resourceDataLoader;
    private readonly ISaveGameLoader _saveGameLoader;
    private readonly List<TradeOffer> _trades = [];
    private readonly BindingList<Ware> _wares = [];

    [AllowNull]
    private IResourceData _resourceData;
    private ISaveGame? _saveGame;
    private Ware? _selectedWare;

    /// <summary>
    /// Creates a new instance of the <see cref="MainForm"/> class with the specified parameters.
    /// </summary>
    /// <param name="debugForm">An <see cref="DebugForm"/> that is the debug form to use.</param>
    /// <param name="resourceDataLoader">An <see cref="IResourceDataLoader"/> that is the resource data loader to use.</param>
    /// <param name="saveGameLoader">An <see cref="ISaveGameLoader"/> that is the save game loader to use.</param>
    public MainForm(
                    DebugForm debugForm,
                    IResourceDataLoader resourceDataLoader,
                    ISaveGameLoader saveGameLoader)
    {
        _debugForm = debugForm;
        _resourceDataLoader = resourceDataLoader;
        _saveGameLoader = saveGameLoader;

        InitializeComponent();

        Load += async (sender, e) => await LoadData();
        WareComboBox.SelectedValueChanged += WareComboBox_SelectedValueChanged;
        _debugForm.Show();
    }

    private void DoRefresh()
    {
        DoRefreshAbandonedShips();
        DoRefreshTrades();
    }

    private void DoRefreshAbandonedShips()
    {
        if (InvokeRequired)
        {
            BeginInvoke(DoRefreshAbandonedShips);
            return;
        }

        AbandonedShipsListView.Items.Clear();
        AbandonedShipsListView.Items.AddRange([.. _abandonedShips.Select(GenerateListViewItem)]);
    }

    private void DoRefreshTrades()
    {
        if (InvokeRequired)
        {
            BeginInvoke(DoRefreshTrades);
            return;
        }

        var buys = _trades
                          .Where(x => x.Type == TradeType.Buy &&
                                      x.Amount > 0)
                          .OrderByDescending(x => x.Price);
        var sells = _trades
                          .Where(x => x.Type == TradeType.Sell &&
                                      x.Amount > 0)
                          .OrderBy(x => x.Price);

        TradesListViewBuy.Items.Clear();
        TradesListViewBuy.Items.AddRange([.. buys.Select(GenerateListViewItem)]);

        TradesListViewSell.Items.Clear();
        TradesListViewSell.Items.AddRange([.. sells.Select(GenerateListViewItem)]);

        if (_selectedWare != null)
        {
            lblAverageBuyPrice.Text = $"Average SELL price: {(float)_selectedWare.AverageBuyPrice / 100:0.00}";
            lblAverageSellPrice.Text = $"Average BUY price: {(float)_selectedWare.AverageSellPrice / 100:0.00}";
        }
        else
        {
            lblAverageBuyPrice.Text = string.Empty;
            lblAverageSellPrice.Text = string.Empty;
        }
    }

    private static ListViewItem GenerateListViewItem(AbandonedShip source)
    {
        var returnValue = new ListViewItem(source.Class.ToString());
        returnValue.SubItems.Add(source.Type);
        returnValue.SubItems.Add(source.SectorName);
        returnValue.SubItems.Add(source.X);
        returnValue.SubItems.Add(source.Y);
        returnValue.SubItems.Add(source.Z);
        return returnValue;
    }

    private ListViewItem GenerateListViewItem(TradeOffer source)
    {
        var averageBuyPrice = (int)(_selectedWare?.AverageBuyPrice ?? 0);
        var averageSellPrice = (int)(_selectedWare?.AverageSellPrice ?? 0);

        var backColour = source.Type switch
        {
            TradeType.Buy => GetAverageGradientColour(source.Price, averageBuyPrice, 0.5),
            TradeType.Sell => GetAverageGradientColour(source.Price, averageSellPrice, -0.5),
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

    public static Color GetAverageGradientColour(double value, double average, double maxDeviationPercent = 0.25)
    {
        if (average == 0)
            return Color.LightGray;

        double deviation = (value - average) / average;
        double t = Math.Clamp(deviation / maxDeviationPercent, -1.0, 1.0);

        double hue = 60 + (60 * t);  

        double saturation = 0.95;
        double lightness = 0.52;

        return ColorFromHsl(hue, saturation, lightness);
    }

    private static Color ColorFromHsl(double hue, double saturation, double lightness)
    {
        double c = (1 - Math.Abs(2 * lightness - 1)) * saturation;
        double x = c * (1 - Math.Abs((hue / 60) % 2 - 1));
        double m = lightness - c / 2;

        double r1 = 0, g1 = 0, b1 = 0;

        if (hue < 60) { r1 = c; g1 = x; }
        else if (hue < 120) { r1 = x; g1 = c; }
        else if (hue < 180) { g1 = c; b1 = x; }
        else if (hue < 240) { g1 = x; b1 = c; }
        else if (hue < 300) { r1 = x; b1 = c; }
        else { r1 = c; b1 = x; }

        return Color.FromArgb(255,
            (int)((r1 + m) * 255),
            (int)((g1 + m) * 255),
            (int)((b1 + m) * 255));
    }

    private IEnumerable<TradeOffer> GetMappedTradeOffers()
        => _saveGame?.Universe.Sectors
                                      .SelectMany(x => x.Stations,
                                                  (sector, station) => new
                                                  {
                                                      Sector = sector,
                                                      Station = station
                                                  })
                                      .SelectMany(x => x.Station.Trades,
                                                  (a, trades) => new
                                                  {
                                                      a.Sector,
                                                      a.Station,
                                                      Trade = trades
                                                  })
                                      .Select(x => Map(x.Sector, x.Station, x.Trade))
            ?? [];

    private void LoadAbandonedShips()
    {
        var ships = _saveGame?.Universe.Sectors
                                               .SelectMany(x => x.Ships,
                                                           (sector, ship) => new
                                                           {
                                                               SectorNameId = sector.NameId,
                                                               Ship = ship
                                                           });
        if (ships?.Any() == true)
        {
            _abandonedShips.Clear();
            _abandonedShips.AddRange(ships
                                          .Select(x => Map(x.SectorNameId, x.Ship))
                                          .OrderBy(x => x.SectorName).ThenBy(x => x.Class));
        }

        DoRefreshAbandonedShips();
    }
    private async Task LoadData()
    {
        _resourceData = await _resourceDataLoader.LoadFrom(@"C:\Users\martin\Desktop\_X4\0001-l044.xml");
        WareComboBox.DataSource = _wares;
        WareComboBox.DisplayMember = nameof(Ware.Name);

        DoRefresh();

        RestartMonitor();
    }

    private async Task LoadSaveGame(string filePath)
    {
        _saveGame = await _saveGameLoader.LoadFrom(filePath);

        foreach (var sector in _saveGame.Universe.Sectors)
        {
            var sectorName = _resourceData.SectorNames[sector.NameId];
            Console.WriteLine($"Sector: {sectorName}, IsKnown: {sector.IsKnown}, Owner: {sector.Owner}");

            if (sector.Ships.Any())
            {
                foreach (var ship in sector.Ships)
                {
                    Console.WriteLine(ship);
                }
            }
        }

        LoadAbandonedShips();
        LoadWares();

        _isLoading = false;

        RestartMonitor();
    }

    private void LoadSelectedWare()
    {
        if (InvokeRequired)
        {
            BeginInvoke(LoadSelectedWare);
            return;
        }

        _trades.Clear();

        if (WareComboBox.SelectedItem is Ware ware)
        {
            _selectedWare = ware;

            var trades = GetMappedTradeOffers().Where(x => x.Ware == ware.Name);
            _trades.AddRange(trades);
        }

        DoRefreshTrades();
    }

    private void LoadWares()
    {
        if (InvokeRequired)
        {
            BeginInvoke(LoadWares);
            return;
        }

        _wares.Clear();

        var trades = GetMappedTradeOffers();
        var buys = trades
                         .Where(x => x.Type == TradeType.Buy)
                         .GroupBy(x => x.Ware)
                         .Select(g => new
                         {
                             Name = g.Key,
                             TotalBuyAmount = g.Sum(x => x.Amount),
                             TotalPrice = g.Sum(x => (long)x.Price * x.Amount) 
                         });
        var sells = trades
                          .Where(x => x.Type == TradeType.Sell)
                          .GroupBy(x => x.Ware)
                          .Select(g => new
                          {
                              Name = g.Key,
                              TotalSellAmount = g.Sum(x => x.Amount),
                              TotalPrice = g.Sum(x => (long)x.Price * x.Amount)
                          });

        var wares = buys
                        .Join(
                              sells,
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

        if (wares?.Any() == true)
        {
            foreach (var ware in wares)
            {
                _wares.Add(ware);
            }
        }
    }

    private System.Threading.Timer? _timer;
    private DateTimeOffset? _lastFileTime = DateTimeOffset.MinValue;
    private bool _isLoading = false;

    private void RestartMonitor()
    {
        Console.WriteLine("Restarting monitor");
        _timer = new System.Threading.Timer(Monitor_Tick, null, 10000, Timeout.Infinite);
    }

    private void Monitor_Tick(object? o)
    {
        if (!_isLoading)
        {
            Console.WriteLine("Monitor_Tick");
            var fileInfo = GetMostRecentFile(@"C:\Users\martin\Documents\Egosoft\X4\48359014\save", "*.xml.gz");
            if (fileInfo?.LastWriteTime > _lastFileTime)
            {
                Console.WriteLine($"Loading {fileInfo.FullName}");

                _isLoading = true;
                _ = LoadSaveGame(fileInfo.FullName);
                _lastFileTime = fileInfo.LastWriteTime;
            }
            else
            {
                RestartMonitor();
            }
        }
        else
        {
            RestartMonitor();
        }
    }

    public static FileInfo? GetMostRecentFile(string folderPath, string filter)
    {
        if (!Directory.Exists(folderPath))
            return null;

        var directory = new DirectoryInfo(folderPath);

        var targetTime = DateTime.Now.AddSeconds(-30);

        var mostRecentFile = directory.GetFiles(filter, SearchOption.TopDirectoryOnly)
                                      .Where(x => !x.FullName.Contains("tmp") && !x.FullName.Contains("temp"))
                                      .Where(x => x.LastWriteTime < targetTime)
                                      .OrderByDescending(f => f.LastWriteTime)
                                      .FirstOrDefault();

        return mostRecentFile;
    }

    #region Event Handlers

    private void WareComboBox_SelectedValueChanged(object? sender, EventArgs e)
    {
        LoadSelectedWare();
    }

    #endregion

    private TradeOffer Map(ISector sector, IStation station, ITrade trade)
    {
        var tradeType = (trade.AmountToBuy, trade.AmountToSell) switch
        {
            ( > 0, _) => TradeType.Buy,
            (_, > 0) => TradeType.Sell,
            _ => TradeType.Unknown
        };

        var amount = tradeType switch
        {
            TradeType.Buy => trade.AmountToBuy,
            TradeType.Sell => trade.AmountToSell,
            _ => 0
        };

        var stationName = station.NameId is not null
            ? _resourceData.Lookup(station.NameId)
            : station.Code;
       
        return new TradeOffer()
        {
            Amount = amount,
            Price = trade.Price,
            SectorName = _resourceData.SectorNames[sector.NameId],
            StationName = stationName,
            Type = tradeType,
            Ware = trade.Ware
        };
    }

    private AbandonedShip Map(int sectorNameId, IShip source)
        => new()
        {
            Class = MapShipClass(source.Class),
            SectorName = _resourceData.SectorNames[sectorNameId],
            Type = source.Macro,
            X = source.Position.X.ToString(),
            Y = source.Position.Y.ToString(),
            Z = source.Position.Z.ToString()
        };

    private static string Map(ISectorPosition source)
        => $"{source.X},{source.Y},{source.Z}";

    private static ShipClass MapShipClass(string source)
        => source switch
        {
            Constants.SaveGameFile.AttributeValue.ShipClass.ExtraLarge => ShipClass.ExtraLarge,
            Constants.SaveGameFile.AttributeValue.ShipClass.Large => ShipClass.Large,
            Constants.SaveGameFile.AttributeValue.ShipClass.Medium => ShipClass.Medium,
            Constants.SaveGameFile.AttributeValue.ShipClass.Small => ShipClass.Small,
            _ => ShipClass.Unknown
        };
}
