using System.ComponentModel;
using MPL.X4.SaveGame.Models;
using MPL.X4.TradeData.UI.Models.SectorPlot;

namespace MPL.X4.TradeData.UI.Forms;

/// <summary>
/// A class that implements a form that shows a sector viewer.
/// </summary>
internal partial class SectorViewerForm : Form
{
    #region Declarations

    private ISectorModel? _sector;

    #endregion

    #region Constructors

    public SectorViewerForm()
    {
        InitializeComponent();
        Initialise();
    }

    #endregion

    #region Methods

    private void Display()
    {
        Text = $"Sector Viewer: {_sector?.Name}";
    }

    private void Initialise()
    {
        // Event handlers
        this.Load += SectorViewerForm_Load;
    }

    private void LoadSectorData()
    {
        SectorPlot.Clear();

        SectorPlot.AddRange(_sector?
                                    .CollectableDrops
                                    .Select(x => new DropPlot(x)));

        SectorPlot.AddRange(_sector?
                                    .Gates
                                    .Select(x => new GatePlot(x)));

        SectorPlot.AddRange(_sector?
                                    .Lockboxes
                                    .Select(x => new LockboxPlot(x)));

        SectorPlot.AddRange(_sector?
                                    .Ships
                                    .Select(x => new ShipPlot(x)));

        SectorPlot.AddRange(_sector?
                                    .Stations
                                    .Select(x => new StationPlot(x)));

        SectorPlot.ResetView();

        Display();
    }

    #endregion

    #region Event Handlers

    private void SectorViewerForm_Load(object? sender, EventArgs e)
    {
        Display();
    }

    #endregion

    #region Properties

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal ISectorModel? Sector
    {
        get
        {
            return _sector;
        }

        set
        {
            _sector = value;
            LoadSectorData();
        }
    }

    #endregion
}
