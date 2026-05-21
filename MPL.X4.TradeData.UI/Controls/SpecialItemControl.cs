using System.ComponentModel;
using MPL.X4.TradeData.UI.Models.SpecialItem;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a special item control for the application.
/// </summary>
internal partial class SpecialItemControl : UserControl
{
    #region Declarations

    private IEnumerable<ISpecialItem> _items = [];

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
        var displayNoItems = _items.Any() == false;

        SpecialItemListView.Visible = !displayNoItems;
        NoItemsLabel.Visible = displayNoItems;
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
        // Event wireup
        Load += SpecialItemControl_Load;

        DoRefresh();
    }

    private void LoadSpecialItems()
    {
        var orderedItems = _items
                                 .OrderBy(x => x.SectorName)
                                 .ThenBy(x => x.Type)
                                 .ThenBy(x => x.Description)
                                 .Select(GenerateListViewItem);

        SpecialItemListView.BeginUpdate();

        SpecialItemListView.Items.Clear();
        SpecialItemListView.Items.AddRange([.. orderedItems]);

        SpecialItemListView.EndUpdate();
   
        DoRefresh();
    }

    #endregion

    #region Event Handlers

    private void SpecialItemControl_Load(object? sender, EventArgs e)
    {
        DoRefresh();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the items to be displayed in the control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IEnumerable<ISpecialItem> Items
    {
        get
        {
            return _items;
        }

        set
        {
            _items = value;
            LoadSpecialItems();
        }
    }

    #endregion
}
