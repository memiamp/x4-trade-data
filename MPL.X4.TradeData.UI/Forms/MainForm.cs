using Microsoft.Extensions.Logging;
using MPL.X4.Catalog.Services;
using MPL.X4.GameResources.Models.Services;
using MPL.X4.SaveGame.Models;
using MPL.X4.TradeData.UI.Configuration;
using MPL.X4.TradeData.UI.Controls;
using MPL.X4.TradeData.UI.Services;

namespace MPL.X4.TradeData.UI.Forms;

/// <summary>
/// A class that implements the main form for the application.
/// </summary>
internal partial class MainForm : Form
{
    #region Declarations

    private readonly ICatalogFileReader _catalogFileReader;
    private readonly DebugForm _debugForm;
    private readonly IFileConfiguration _fileConfiguration;
    private readonly IGameDataService _gameDataService;
    private readonly ILogger _logger;
    private readonly ISaveGameFileSystemMonitor _saveGameFileSystemMonitor;

    private ISaveGameModels? _saveGame;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="MainForm"/> class with the specified parameters.
    /// </summary>
    /// <param name="debugForm">An <see cref="DebugForm"/> that is the debug form to use.</param>
    /// <param name="fileConfiguration">An <see cref="IFileConfiguration"/> that is the file configuration to use.</param>
    /// <param name="gameDataService">An <see cref="IGameDataService"/> that is the game data service to use.</param>
    /// <param name="gameResourceModelLoader">An <see cref="IGameResourceModelLoader"/> that is the game resource model loader to use.</param>
    /// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
    /// <param name="modelMapper">An <see cref="IModelMapper"/> that is the model mapper to use.</param>
    /// <param name="saveGameFileSystemMonitor">An <see cref="ISaveGameFileSystemMonitor"/> that is the save game file system monitor to use.</param>
    public MainForm(
                    ICatalogFileReader catalogFileReader,
                    DebugForm debugForm,
                    IFileConfiguration fileConfiguration,
                    IGameDataService gameDataService,
                    ILogger<MainForm> logger,
                    ISaveGameFileSystemMonitor saveGameFileSystemMonitor)
    {
        _catalogFileReader = catalogFileReader;
        _debugForm = debugForm;
        _fileConfiguration = fileConfiguration;
        _gameDataService = gameDataService;
        _logger = logger;
        _saveGameFileSystemMonitor = saveGameFileSystemMonitor;

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
        Load += async (sender, e) => await LoadData();
        SectorListControl.ViewSectorRequest += SectorListControl_ViewSectorRequest;
        _saveGameFileSystemMonitor.SaveGameFileUpdated += SaveGameFileSystemMonitor_SaveGameFileUpdated;

        _debugForm.Show();
    }

    private async Task LoadData()
    {
        await _gameDataService.ReloadResources();

        _saveGameFileSystemMonitor.Start();
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

    private void UpdateSpecialItemsControl()
    {
        //SpecialItemControl.Items = _saveGame?.Universe.Sectors.Any() == true
        //    ? _modelMapper.MapSpecialItems(_saveGame.Universe.Sectors, _resourceData)
        //    : [];
    }

    private void UpdateTradeControl()
    {
        //TradeControl.Items = _saveGame?.Universe.Sectors.Any() == true
        //    ? _modelMapper.MapTradeOffers(_saveGame.Universe.Sectors, _resourceData)
        //    : [];
    }

    #endregion

    #region Event Handlers

    private async void SaveGameFileSystemMonitor_SaveGameFileUpdated(object? sender, SaveGameFileUpdatedEventArgs e)
    {
        await LoadSaveGame(e.SaveGameFilePath);
    }

    private void SectorListControl_ViewSectorRequest(object? sender, SectorActionEventArgs e)
    {
        ShowSectorView(e.Sector);
    }

    #endregion
}
