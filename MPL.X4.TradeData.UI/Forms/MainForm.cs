using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using MPL.X4.Services;
using MPL.X4.TradeData.Services;
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
    private readonly ILogger _logger;
    private readonly IModelMapper _modelMapper;
    private readonly IResourceDataLoader _resourceDataLoader;
    private readonly ISaveGameLoader _saveGameLoader;
    private readonly ISaveGameFileSystemMonitor _saveGameFileSystemMonitor;

    [AllowNull]
    private IResourceData _resourceData;
    private ISaveGame? _saveGame;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="MainForm"/> class with the specified parameters.
    /// </summary>
    /// <param name="debugForm">An <see cref="DebugForm"/> that is the debug form to use.</param>
    /// <param name="fileConfiguration">An <see cref="IFileConfiguration"/> that is the file configuration to use.</param>
    /// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
    /// <param name="modelMapper">An <see cref="IModelMapper"/> that is the model mapper to use.</param>
    /// <param name="resourceDataLoader">An <see cref="IResourceDataLoader"/> that is the resource data loader to use.</param>
    /// <param name="saveGameFileSystemMonitor">An <see cref="ISaveGameFileSystemMonitor"/> that is the save game file system monitor to use.</param>
    /// <param name="saveGameLoader">An <see cref="ISaveGameLoader"/> that is the save game loader to use.</param>
    public MainForm(
                    ICatalogFileReader catalogFileReader,
                    DebugForm debugForm,
                    IFileConfiguration fileConfiguration,
                    ILogger<MainForm> logger,
                    IModelMapper modelMapper,
                    IResourceDataLoader resourceDataLoader,
                    ISaveGameFileSystemMonitor saveGameFileSystemMonitor,
                    ISaveGameLoader saveGameLoader)
    {
        _catalogFileReader = catalogFileReader;
        _debugForm = debugForm;
        _fileConfiguration = fileConfiguration;
        _logger = logger;
        _modelMapper = modelMapper;
        _resourceDataLoader = resourceDataLoader;
        _saveGameFileSystemMonitor = saveGameFileSystemMonitor;
        _saveGameLoader = saveGameLoader;

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
        var s = await _catalogFileReader.ReadTextFile(@"C:\Program Files (x86)\Steam\steamapps\common\X4 Foundations", 9, "044.xml");
        Console.WriteLine(s.Length);
        _resourceData = await _resourceDataLoader.LoadFrom(_fileConfiguration.TextResourceFilePath);

        _saveGameFileSystemMonitor.Start();
    }

    private async Task LoadSaveGame(string filePath)
    {
        _logger.LogInformation("Loading save game file from {SaveGameFilePath}", filePath);

        _saveGame = await _saveGameLoader.LoadFrom(filePath);

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

    private void ShowSectorView(string sectorCode)
    {
        if (InvokeRequired)
        {
            BeginInvoke(() => ShowSectorView(sectorCode));
            return;
        }

        var sector = _saveGame?.Universe.Sectors.FirstOrDefault(x => x.Code == sectorCode);
        if (sector is not null)
        {
            var sectorViewer = new SectorViewerForm
            {
                Sector = sector
            };
            sectorViewer.Show();
        }
        else
        {
            MessageBox.Show($"The sector {sectorCode} could not be found", "View Sector", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void UpdateSectorListControl()
    {
        SectorListControl.Items = _saveGame?.Universe.Sectors.Any() == true
            ? _modelMapper.MapSectors(_saveGame.Universe.Sectors, _resourceData)
            : [];
    }

    private void UpdateSpecialItemsControl()
    {
        SpecialItemControl.Items = _saveGame?.Universe.Sectors.Any() == true
            ? _modelMapper.MapSpecialItems(_saveGame.Universe.Sectors, _resourceData)
            : [];
    }

    private void UpdateTradeControl()
    {
        TradeControl.Items = _saveGame?.Universe.Sectors.Any() == true
            ? _modelMapper.MapTradeOffers(_saveGame.Universe.Sectors, _resourceData)
            : [];
    }

    #endregion

    #region Event Handlers

    private async void SaveGameFileSystemMonitor_SaveGameFileUpdated(object? sender, SaveGameFileUpdatedEventArgs e)
    {
        await LoadSaveGame(e.SaveGameFilePath);
    }

    private void SectorListControl_ViewSectorRequest(object? sender, SectorActionEventArgs e)
    {
        ShowSectorView(e.SectorCode);
    }

    #endregion
}
