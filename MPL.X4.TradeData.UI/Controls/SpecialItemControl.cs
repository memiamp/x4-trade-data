using System.ComponentModel;
using MPL.X4.TradeData.UI.Models.SpecialItem;
using MPL.X4.TradeData.UI.Services;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a special item control for the application.
/// </summary>
internal partial class SpecialItemControl : UserControl
{
    #region Declarations

    private ISpecialItemDataProvider? _dataProvider;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="SpecialItemControl"/> class.
    /// </summary>
    public SpecialItemControl()
    {
        InitializeComponent();
        Initialise();
    }

    #endregion

    #region Methods

    private void DoRefresh()
    {
        var hasItems = _dataProvider?.HasSaveGame == true;

        NoItemsLabel.Visible = !hasItems;
    
        AbandonedShipCheckBox.Visible = hasItems;
        BuildStorageAbandonedCheckBox.Visible = hasItems;
        BuildStorageTopCheckBox.Visible = hasItems;
        BuildStorageTopNumericUpDown.Visible = hasItems;
        LockboxCheckBox.Visible = hasItems;

        SpecialItemListView.Visible = hasItems;
    }

    private static ListViewItem GenerateListViewItem(ISpecialItem source)
    {
        var returnValue = new ListViewItem(GetSpecialItemType(source.Type))
        {
            BackColor = source.Colour
        };
        returnValue.SubItems.Add(source.Description);
        returnValue.SubItems.Add(source.SectorName);
        returnValue.SubItems.Add(source.X);
        returnValue.SubItems.Add(source.Y);
        returnValue.SubItems.Add(source.Z);
        returnValue.SubItems.Add(source.Comments);
        return returnValue;
    }

    private static string GetSpecialItemType(SpecialItemType source)
        => source switch
        {
            SpecialItemType.BuildStorage => Constants.SpecialItemTypes.BuildStorage,
            SpecialItemType.Lockbox => Constants.SpecialItemTypes.Lockbox,
            SpecialItemType.ShipExtraLarge => Constants.SpecialItemTypes.ShipExtraLarge,
            SpecialItemType.ShipExtraSmall => Constants.SpecialItemTypes.ShipExtraSmall,
            SpecialItemType.ShipLarge => Constants.SpecialItemTypes.ShipLarge,
            SpecialItemType.ShipMedium => Constants.SpecialItemTypes.ShipMedium,
            SpecialItemType.ShipSmall => Constants.SpecialItemTypes.ShipSmall,
            _ => string.Empty
        };

    private void Initialise()
    {
        AbandonedShipCheckBox.Checked = true;
        BuildStorageAbandonedCheckBox.Checked = true;
        BuildStorageTopCheckBox.Checked = true;
        LockboxCheckBox.Checked = true;

        // Event wireup
        Load += SpecialItemControl_Load;
        AbandonedShipCheckBox.CheckedChanged += AbandonedShipCheckBox_CheckedChanged;
        BuildStorageAbandonedCheckBox.CheckedChanged += BuildStorageAbandonedCheckBox_CheckedChanged;
        BuildStorageTopCheckBox.CheckedChanged += BuildStorageTopCheckBox_CheckedChanged;
        BuildStorageTopNumericUpDown.ValueChanged += BuildStorageTopNumericUpDown_ValueChanged;
        LockboxCheckBox.CheckedChanged += LockboxCheckBox_CheckedChanged;

        DoRefresh();
    }

    internal void LoadData()
    {
        var items = new List<ISpecialItem>();

        if (_dataProvider is not null)
        {
            if (AbandonedShipCheckBox.Checked)
            {
                items.AddRange(_dataProvider.GetAbandonedShips());
            }

            if (BuildStorageAbandonedCheckBox.Checked)
            {
                items.AddRange(_dataProvider.GetAbandonedBuildStorages());
            }

            if (BuildStorageTopCheckBox.Checked)
            {
                items.AddRange(_dataProvider.GetTopBuildStorages((int)BuildStorageTopNumericUpDown.Value));
            }

            if (LockboxCheckBox.Checked)
            {
                items.AddRange(_dataProvider.GetLockboxes());
            }

            var orderedItems = items
                                    .OrderBy(x => x.SectorName)
                                    .ThenBy(x => x.Type)
                                    .ThenBy(x => x.Description)
                                    .Select(GenerateListViewItem);

            SpecialItemListView.BeginUpdate();

            SpecialItemListView.Items.Clear();
            SpecialItemListView.Items.AddRange([.. orderedItems]);

            SpecialItemListView.EndUpdate();
        }

        DoRefresh();
    }

    #endregion

    #region Event Handlers

    private void AbandonedShipCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void BuildStorageAbandonedCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void BuildStorageTopCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void BuildStorageTopNumericUpDown_ValueChanged(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void LockboxCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void SpecialItemControl_Load(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the data provider for the control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal ISpecialItemDataProvider? DataProvider
    {
        get
        {
            return _dataProvider;
        }

        set
        {
            _dataProvider = value;
            LoadData();
        }
    }

    #endregion
}
