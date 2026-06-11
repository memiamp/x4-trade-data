using System.ComponentModel;
using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Controls;

/// <summary>
/// A class that implements a ship browser control for the application.
/// </summary>
internal partial class ShipBrowserControl : UserControl
{
    #region Declarations

    private IEnumerable<IShipModel> _items = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="ShipBrowserControl"/> class.
    /// </summary>
    public ShipBrowserControl()
    {
        InitializeComponent();
        Initialise();
    }

    #endregion

    #region Methods

    private void DoRefresh()
    {
        var hasItems = _items.Any();

        NoItemsLabel.Visible = !hasItems;
        AbandonedCheckBox.Visible = hasItems;
        CargoCheckBox.Visible = hasItems;
        ModificationCheckBox.Visible = hasItems;
        ShipListView.Visible = hasItems;
    }

    private static ListViewItem GenerateListViewItem(IShipModel source)
    {
        var returnValue = new ListViewItem(GetShipClass(source));
        returnValue.SubItems.Add(source.Model);
        returnValue.SubItems.Add(source.Owner.Acronym);
        returnValue.SubItems.Add("Unknown");
        //returnValue.SubItems.Add(source.SectorName);
        returnValue.SubItems.Add(GetCargo(source));
        returnValue.SubItems.Add(GetModifications(source));
        returnValue.SubItems.Add(GetPosition(source.Transform));

        return returnValue;
    }

    private static string GetCargo(IShipModel source)
    {
        var cargoItems = source
                               .Cargo
                               .Where(x => x.Amount > 0);
        if (cargoItems.Any())
        {
            var count = cargoItems.Count();
            var totalAmount = cargoItems.Sum(x => x.Amount);

            return $"{totalAmount:#,##0} ({count} ware(s))";
        }

        return string.Empty;
    }

    private static void GetModificationQuality(IModificationModel? source, ref int basic, ref int enhanced, ref int exceptional)
    {
        if (source?.Quality == ModificationQuality.Basic)
            basic++;
        else if (source?.Quality == ModificationQuality.Enhanced)
            enhanced++;
        else if (source?.Quality == ModificationQuality.Exceptional)
            exceptional++;
    }

    private static void GetModificationQuality(IEnumerable<IModificationModel> source, ref int basic, ref int enhanced, ref int exceptional)
    {
        foreach (var item in source)
        {
            GetModificationQuality(item, ref basic, ref enhanced, ref exceptional);
        }
    }

    private static string GetModifications(IShipModel source)
    {
        var basicModifications = 0;
        var enhancedModifications = 0;
        var exceptionModifications = 0;
        var returnValue = string.Empty;

        GetModificationQuality(source.EngineModification, ref basicModifications, ref enhancedModifications, ref exceptionModifications);
        GetModificationQuality(source.PaintModification, ref basicModifications, ref enhancedModifications, ref exceptionModifications);
        GetModificationQuality(source.ShieldModification, ref basicModifications, ref enhancedModifications, ref exceptionModifications);
        GetModificationQuality(source.ShipModification, ref basicModifications, ref enhancedModifications, ref exceptionModifications);
        GetModificationQuality(source.WeaponModifications, ref basicModifications, ref enhancedModifications, ref exceptionModifications);

        if (exceptionModifications > 0)
        {
            returnValue += $"{exceptionModifications} Exceptional, ";
        }
        if (enhancedModifications > 0)
        {
            returnValue += $"{enhancedModifications} Enhanced, ";
        }
        if (basicModifications > 0)
        {
            returnValue += $"{basicModifications} Basic";
        }

        return returnValue.Trim(' ', ',');
    }

    private static string GetPosition(ITransform3D source)
        => $"{source.Position.X:0},{source.Position.Y:0},{source.Position.Z:0}";

    private static string GetShipClass(IShipModel source)
        => source.Class switch
        {
            ShipClass.ExtraLarge => Constants.ShipClass.ExtraLarge,
            ShipClass.ExtraSmall => Constants.ShipClass.ExtraSmall,
            ShipClass.Large => Constants.ShipClass.Large,
            ShipClass.Medium => Constants.ShipClass.Medium,
            ShipClass.Small => Constants.ShipClass.Small,
            _ => "Unknown"
        };

    private static bool HasModifications(IShipModel source, bool includePaint = false)
        => source.EngineModification is not null ||
           source.ShieldModification is not null ||
           source.ShipModification is not null ||
           source.WeaponModifications.Any() ||
           includePaint &&
           source.PaintModification is not null;

    private void Initialise()
    {
        AbandonedCheckBox.Checked = true;
        CargoCheckBox.Checked = false;
        ModificationCheckBox.Checked = false;

        // Event wireup
        Load += SpecialItemControl_Load;
        AbandonedCheckBox.CheckedChanged += AbandonedCheckBox_CheckedChanged;
        CargoCheckBox.CheckedChanged += CargoCheckBox_CheckedChanged;
        ModificationCheckBox.CheckedChanged += ModificationCheckBox_CheckedChanged;

        DoRefresh();
    }

    internal void LoadData()
    {
        IEnumerable<ListViewItem> items = [];

        if (_items is not null)
        {
            var sourceItems = _items;

            if (AbandonedCheckBox.Checked)
            {
                sourceItems = sourceItems.Where(x => x.CanBeCaptured);
            }

            if (CargoCheckBox.Checked)
            {
                sourceItems = sourceItems.Where(x => x.Cargo.Any());
            }

            if (ModificationCheckBox.Checked)
            {
                sourceItems = sourceItems.Where(x => HasModifications(x));
            }

            items = sourceItems
                               .OrderBy(x => x.Name ?? x.Model)
                               .ThenBy(x => x.Owner.Acronym)
                               .Select(GenerateListViewItem);
        
        }

        ShipListView.BeginUpdate();

        ShipListView.Items.Clear();
        ShipListView.Items.AddRange([.. items]);

        ShipListView.EndUpdate();

        DoRefresh();
    }

    #endregion

    #region Event Handlers

    private void AbandonedCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void CargoCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        LoadData();
    }

    private void ModificationCheckBox_CheckedChanged(object? sender, EventArgs e)
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
    /// Gets or sets the items for the control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IEnumerable<IShipModel> Items
    {
        get
        {
            return _items;
        }

        set
        {
            _items = value;
            LoadData();
        }
    }

    #endregion
}
