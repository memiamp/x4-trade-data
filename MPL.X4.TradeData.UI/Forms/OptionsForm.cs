using Microsoft.Extensions.Logging;
using MPL.X4.TradeData.UI.Configuration;

namespace MPL.X4.TradeData.UI.Forms;

/// <summary>
/// A class that implements the options form for the application.
/// </summary>
internal partial class OptionsForm : Form
{
    #region Declarations

    private readonly IFileConfiguration _fileConfiguration;
    private readonly ILogger _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="OptionsForm"/> class with the specified parameters.
    /// </summary>
    /// <param name="fileConfiguration">An <see cref="IFileConfiguration"/> that is the file configuration to use.</param>
    /// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
    public OptionsForm(
                       IFileConfiguration fileConfiguration,
                       ILogger<OptionsForm> logger)
    {
        _fileConfiguration = fileConfiguration;
        _logger = logger;

        InitializeComponent();
        Initialise();
    }

    #endregion

    #region Methods

    private bool HasDataChanged()
        => CatalogFilePathTextBox.Text != _fileConfiguration.CatalogFilePath ||
           CheckIntervalSecondsNumericUpDown.Value != _fileConfiguration.CheckIntervalSeconds ||
           SaveGameFileAgeSecondsNumericUpDown.Value != _fileConfiguration.SaveGameFileAgeSeconds ||
           SaveGameFileFilterTextBox.Text != _fileConfiguration.SaveGameFileFilter ||
           SaveGameFilePathTextBox.Text != _fileConfiguration.SaveGameFilePath;

    private void Initialise()
    {
        // Event handlers
        Load += OptionsForm_Load;
        CancelChangeButton.Click += CancelChangeButton_Click;
        CatalogFilePathBrowseButton.Click += CatalogFilePathBrowseButton_Click;
        OkButton.Click += OkButton_Click;
        SaveGameFilePathBrowseButton.Click += SaveGameFilePathBrowseButton_Click;
    }

    #endregion

    #region Methods

    private void LoadData()
    {
        CatalogFilePathTextBox.Text = _fileConfiguration.CatalogFilePath;
        CheckIntervalSecondsNumericUpDown.Value = _fileConfiguration.CheckIntervalSeconds;
        SaveGameFileAgeSecondsNumericUpDown.Value = _fileConfiguration.SaveGameFileAgeSeconds;
        SaveGameFileFilterTextBox.Text = _fileConfiguration.SaveGameFileFilter;
        SaveGameFilePathTextBox.Text = _fileConfiguration.SaveGameFilePath;
    }

    private void SaveData()
    {
        _logger.LogInformation("Saving updated configuration");

        _fileConfiguration.CatalogFilePath = CatalogFilePathTextBox.Text;
        _fileConfiguration.CheckIntervalSeconds = (uint)CheckIntervalSecondsNumericUpDown.Value;
        _fileConfiguration.SaveGameFileAgeSeconds = (uint)SaveGameFileAgeSecondsNumericUpDown.Value;
        _fileConfiguration.SaveGameFileFilter = SaveGameFileFilterTextBox.Text;
        _fileConfiguration.SaveGameFilePath = SaveGameFilePathTextBox.Text;

        _fileConfiguration.Save();
    }

    #endregion

    #region Event Handlers

    private void CancelChangeButton_Click(object? sender, EventArgs e)
    {
        if (HasDataChanged())
        {
            var result = MessageBox.Show("Configuration has changed, do you want to save changes?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.No)
            {
                Close(DialogResult.Cancel);
            }
            else if (result == DialogResult.Yes)
            {
                SaveData();
                Close(DialogResult.OK);
            }
        }
        else
        {
            Close(DialogResult.Cancel);
        }
    }

    private void CatalogFilePathBrowseButton_Click(object? sender, EventArgs e)
    {
        var browser = new FolderBrowserDialog
        {
            Description = "Select the catalog file path",
            InitialDirectory = CatalogFilePathTextBox.Text
        };

        if (browser.ShowDialog() == DialogResult.OK)
        {
            CatalogFilePathTextBox.Text = browser.SelectedPath;
        }
    }

    private void Close(DialogResult result)
    {
        DialogResult = result;
        Close();
    }

    private void OkButton_Click(object? sender, EventArgs e)
    {
        if (HasDataChanged())
        {
            SaveData();
        }

        Close(DialogResult.OK);
    }

    private void OptionsForm_Load(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void SaveGameFilePathBrowseButton_Click(object? sender, EventArgs e)
    {
        var browser = new FolderBrowserDialog
        {
            Description = "Select the save game file path",
            InitialDirectory = SaveGameFilePathTextBox.Text
        };

        if (browser.ShowDialog() == DialogResult.OK)
        {
            SaveGameFilePathTextBox.Text = browser.SelectedPath;
        }
    }

    #endregion
}
