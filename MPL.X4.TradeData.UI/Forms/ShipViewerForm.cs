using MPL.X4.SaveGame.Models;
using MPL.X4.TradeData.UI.Models;
using MPL.X4.TradeData.UI.Services;

namespace MPL.X4.TradeData.UI.Forms;

/// <summary>
/// A class that implements a form that views a ship.
/// </summary>
internal partial class ShipViewerForm : Form
{
    #region Constructors

    private ShipViewerForm()
    {
        InitializeComponent();
        Initialise();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ShipViewerForm"/> with the specified parameters.
    /// </summary>
    /// <param name="ship">A <see cref="ShipBrowserListItem"/> that is the ship to view.</param>
    internal ShipViewerForm(
                            ShipBrowserListItem ship)

        : this()
    {
        BackColor = ship.Ship.Owner.Colour.Colour;
        CanBeCapturedCheckBox.Checked = ship.CanBeCaptured;
        ClassTextBox.Text = ship.Class;
        CodeTextBox.Text = ship.Ship.Code;
        IsKnownCheckBox.Checked = ship.Ship.IsKnown;
        IsWreckCheckBox.Checked = ship.Ship.IsWreck;
        ModelTextBox.Text = ship.Model;
        NameTextBox.Text = ship.Name;
        OwnerTextBox.Text = ship.Ship.Owner.Name;
        PositionXTextBox.Text = ship.X;
        PositionYTextBox.Text = ship.Y;
        PositionZTextBox.Text = ship.Z;
        RotationPitchTextBox.Text = ship.Ship.Transform.Rotation.Pitch.ToString("0");
        RotationRollTextBox.Text = ship.Ship.Transform.Rotation.Roll.ToString("0");
        RotationYawTextBox.Text = ship.Ship.Transform.Rotation.Yaw.ToString("0");
        SectorTextBox.Text = ship.Location;
        Text = $"Ship Viewer: {ship.Name} ({ship.Ship.Code})";

        LoadCargo(ship.Ship.Cargo);
        LoadModifications(ship.Ship);
    }

    #endregion

    #region Methods

    private static ListViewItem GenerateListViewItem(ICargoItemModel source)
    {
        var returnValue = new ListViewItem(source.Name);
        returnValue.SubItems.Add($"{source.Amount:#,##0}");
        returnValue.SubItems.Add($"{source.Value:#,##0}Cr");

        returnValue.Tag = source;

        return returnValue;
    }

    private static ListViewItem GenerateListViewItem(IModificationModel source)
    {
        var name = source.Name;
        if (name.Contains('('))
        {
            name = name[..name.IndexOf('(')].Trim();
        }
        var returnValue = new ListViewItem(source.Type.ToString());
        returnValue.SubItems.Add(name);
        returnValue.SubItems.Add(IMapper.Map(source.Quality));

        returnValue.Tag = source;

        return returnValue;
    }

    private void Initialise()
    {
        OkButton.Click += OkButton_Click;
    }

    private void LoadCargo(ICargoItemModelList items)
    {
        CargoListView.BeginUpdate();

        CargoListView.Items.Clear();

        foreach (var item in items.OrderBy(x => x.Name))
        {
            CargoListView.Items.Add(GenerateListViewItem(item));
        }

        CargoListView.EndUpdate();
    }

    private void LoadModifications(IShipModel ship)
    {
        ModificationsListView.BeginUpdate();

        ModificationsListView.Items.Clear();

        if (ship.EngineModification is not null)
            ModificationsListView.Items.Add(GenerateListViewItem(ship.EngineModification));

        if (ship.PaintModification is not null)
            ModificationsListView.Items.Add(GenerateListViewItem(ship.PaintModification));

        if (ship.ShieldModification is not null)
            ModificationsListView.Items.Add(GenerateListViewItem(ship.ShieldModification));

        if (ship.ShipModification is not null)
            ModificationsListView.Items.Add(GenerateListViewItem(ship.ShipModification));

        foreach (var item in ship.WeaponModifications.OrderBy(x => x.Name))
        {
            ModificationsListView.Items.Add(GenerateListViewItem(item));
        }

        ModificationsListView.EndUpdate();
    }

    #endregion

    #region Event Handlers

    private void OkButton_Click(object? sender, EventArgs e)
    {
        Close();
    }

    #endregion
}
