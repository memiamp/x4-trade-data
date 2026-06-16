using Microsoft.Extensions.Logging;
using MPL.X4.SaveGame.Models;
using MPL.X4.SaveGame.Models.Services;
using MPL.X4.TradeData.UI.Controls;
using MPL.X4.TradeData.UI.Models;
using MPL.X4.TradeData.UI.Models.Trade;
using MPL.X4.TradeData.UI.Services;

namespace MPL.X4.TradeData.UI.Forms;

/// <summary>
/// A class that implements the main form for the application.
/// </summary>
internal partial class MainForm : Form
{
    #region Declarations

    private readonly DebugForm _debugForm;
    private readonly IGameDataService _gameDataService;
    private readonly ILogger _logger;
    private readonly OptionsForm _optionsForm;
    private readonly ISaveGameFileSystemMonitor _saveGameFileSystemMonitor;
    private readonly ISpecialItemDataProvider _specialItemDataProvider;

    private ISaveGameModels? _saveGame;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="MainForm"/> class with the specified parameters.
    /// </summary>
    /// <param name="debugForm">A <see cref="DebugForm"/> that is the debug form to use.</param>
    /// <param name="gameDataService">An <see cref="IGameDataService"/> that is the game data service to use.</param>
    /// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
    /// <param name="optionsForm">A <see cref="OptionsForm"/> that is the options form to use.</param>
    /// <param name="saveGameFileSystemMonitor">An <see cref="ISaveGameFileSystemMonitor"/> that is the save game file system monitor to use.</param>
    /// <param name="specialItemDataProvider">An <see cref="ISpecialItemDataProvider"/> that is the special item data provider to use.</param>
    public MainForm(
                    DebugForm debugForm,
                    IGameDataService gameDataService,
                    ILogger<MainForm> logger,
                    OptionsForm optionsForm,
                    ISaveGameFileSystemMonitor saveGameFileSystemMonitor,
                    ISpecialItemDataProvider specialItemDataProvider)
    {
        _debugForm = debugForm;
        _gameDataService = gameDataService;
        _logger = logger;
        _optionsForm = optionsForm;
        _saveGameFileSystemMonitor = saveGameFileSystemMonitor;
        _specialItemDataProvider = specialItemDataProvider;

        InitializeComponent();
        Initialise();
    }

    #endregion

    #region Methods

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
            _saveGameFileSystemMonitor.Dispose();
        }

        base.Dispose(disposing);
    }

    private void Initialise()
    {
        // Event handlers
        FileMenu_Exit.Click += FileMenu_Exit_Click;
        Load += async (sender, e) => await LoadData();
        SectorListControl.ViewSectorRequest += SectorListControl_ViewSectorRequest;
        _saveGameFileSystemMonitor.SaveGameFileUpdated += SaveGameFileSystemMonitor_SaveGameFileUpdated;
        SpecialItemControl.DataProvider = _specialItemDataProvider;
        ToolMenu_Options.Click += ToolMenu_Options_Click;

        _debugForm.Show();
    }

    private async Task LoadData()
    {
        var isLoaded = false;

        while (!isLoaded)
        {
            try
            {
                await _gameDataService.ReloadResources();

                _saveGameFileSystemMonitor.Start();

                isLoaded = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not load data");

                MessageBox.Show("Could not load resources. Please ensure options are set", "Load Resources", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (_optionsForm.ShowDialog() == DialogResult.Cancel)
                {
                    Application.Exit();
                }
            }
        }
    }

    private async Task LoadSaveGame(string filePath)
    {
        _logger.LogInformation("Loading save game file from {SaveGameFilePath}", filePath);

        _saveGame = await _gameDataService.LoadSaveGame(filePath);
        
        OnUpdateAfterSaveGameLoaded();
    }

    private void OnUpdateAfterSaveGameLoaded()
    {
        if (InvokeRequired)
        {
            BeginInvoke(OnUpdateAfterSaveGameLoaded);
            return;
        }
        
        UpdateSectorListControl();
        UpdateShipControl();
        UpdateSpecialItemsControl();
        UpdateTradeControl();
    }

    private void ShowSectorView(ISectorModel sector)
    {
        if (InvokeRequired)
        {
            BeginInvoke(() => ShowSectorView(sector));
            return;
        }

        var sectorViewer = new SectorViewerForm
        {
            Sector = sector
        };
        sectorViewer.Show();
    }

    private void UpdateSectorListControl()
    {
        SectorListControl.Items = _saveGame?.Universe.Sectors.Any() == true
            ? _saveGame.Universe.Sectors
            : [];
    }

    private void UpdateShipControl()
    {
        if (_saveGame is not null)
        {
            var ships = _saveGame
                                 .Universe
                                 .Sectors
                                 .SelectMany(
                                             x => x.GetAllShips(),
                                             (s, x) => new ShipBrowserListItem(s, x));

            var highwayShips = _saveGame
                                        .Universe
                                        .HighwayShips
                                        .Flatten()
                                        .Select(x => new ShipBrowserListItem("Superhighway", x));

            ShipControl.Items = ships.Concat(highwayShips);
        }
    }

    private void UpdateSpecialItemsControl()
    {
        _specialItemDataProvider.SaveGame = _saveGame;
        SpecialItemControl.LoadData();
    }

    private void UpdateTradeControl()
    {
        if (_saveGame?.Universe.Sectors.Any() == true)
        {
            var trades = _saveGame
                                  .Universe
                                  .Sectors
                                  .SelectMany(se => se.Stations
                                                               .SelectMany(st => st.Trades
                                                                                          .Select(t => new StationTradeListItem(se, st, t))));

            TradeControl.Items = trades;
        }
        else
        {
            TradeControl.Items = [];
        }
    }

    #endregion

    #region Event Handlers

    private void FileMenu_Exit_Click(object? sender, EventArgs e)
    {
        Application.Exit();
    }

    private async void SaveGameFileSystemMonitor_SaveGameFileUpdated(object? sender, SaveGameFileUpdatedEventArgs e)
    {
        await LoadSaveGame(e.SaveGameFilePath);
    }

    private void SectorListControl_ViewSectorRequest(object? sender, SectorActionEventArgs e)
    {
        ShowSectorView(e.Sector);
    }

    private void ToolMenu_Options_Click(object? sender, EventArgs e)
    {
        _optionsForm.ShowDialog();
    }

    #endregion
}
