using System.Diagnostics.CodeAnalysis;
using MPL.X4.Services;
using MPL.X4.TradeData.Services;
using MPL.X4.TradeData.UI.Models;
using MPL.X4.TradeData.UI.Services;

namespace MPL.X4.TradeData.UI.Forms;

/// <summary>
/// A class that implements the main form for the application.
/// </summary>
internal partial class MainForm : Form
{
    #region Declarations

    private readonly DebugForm _debugForm;
    private readonly IModelMapper _modelMapper;
    private readonly IResourceDataLoader _resourceDataLoader;
    private readonly ISaveGameLoader _saveGameLoader;

    [AllowNull]
    private IResourceData _resourceData;
    private string _saveFilePath = @"C:\Users\martin\Documents\Egosoft\X4\48359014\save";
    private ISaveGame? _saveGame;
    private string _textResourceFile = @"C:\Users\martin\Desktop\_X4\0001-l044.xml";

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="MainForm"/> class with the specified parameters.
    /// </summary>
    /// <param name="debugForm">An <see cref="DebugForm"/> that is the debug form to use.</param>
    /// <param name="modelMapper">An <see cref="IModelMapper"/> that is the model mapper to use.</param>
    /// <param name="resourceDataLoader">An <see cref="IResourceDataLoader"/> that is the resource data loader to use.</param>
    /// <param name="saveGameLoader">An <see cref="ISaveGameLoader"/> that is the save game loader to use.</param>
    public MainForm(
                    DebugForm debugForm,
                    IModelMapper modelMapper,
                    IResourceDataLoader resourceDataLoader,
                    ISaveGameLoader saveGameLoader)
    {
        _debugForm = debugForm;
        _modelMapper = modelMapper;
        _resourceDataLoader = resourceDataLoader;
        _saveGameLoader = saveGameLoader;

        InitializeComponent();

        Load += async (sender, e) => await LoadData();
        _debugForm.Show();
    }

    #endregion

    #region Methods

    private async Task LoadData()
    {
        _resourceData = await _resourceDataLoader.LoadFrom(_textResourceFile);

        RestartMonitor();
    }

    private async Task LoadSaveGame(string filePath)
    {
        _saveGame = await _saveGameLoader.LoadFrom(filePath);

        UpdateSpecialItemsControl();
        UpdateTradeControl();

        _isLoading = false;

        RestartMonitor();
    }

    private void UpdateSpecialItemsControl()
    {
        if (InvokeRequired)
        {
            BeginInvoke(UpdateSpecialItemsControl);
            return;
        }

        SpecialItemControl.SpecialItems = _saveGame?.Universe.Sectors.Any() == true
            ? _modelMapper.MapSpecialItems(_saveGame.Universe.Sectors, _resourceData)
            : [];
    }

    private void UpdateTradeControl()
    {
        if (InvokeRequired)
        {
            BeginInvoke(UpdateTradeControl);
            return;
        }

        TradeControl.TraderOffers = _saveGame?.Universe.Sectors.Any() == true
            ? _modelMapper.MapTradeOffers(_saveGame.Universe.Sectors, _resourceData)
            : [];
    }

    private System.Threading.Timer? _timer;
    private DateTimeOffset? _lastFileTime = DateTimeOffset.MinValue;
    private bool _isLoading = false;

    private void RestartMonitor()
    {
        Console.WriteLine("Restarting monitor");
        _timer = new System.Threading.Timer(Monitor_Tick, null, 10000, Timeout.Infinite);
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

    
    #endregion

    #region Event Handlers

    private void Monitor_Tick(object? o)
    {
        if (!_isLoading)
        {
            Console.WriteLine("Monitor_Tick");
            var fileInfo = GetMostRecentFile(_saveFilePath, "*.xml.gz");
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

    #endregion
}
