using MPL.X4.SaveGame.Models;

namespace MPL.X4.TradeData.UI.Models;

/// <summary>
/// A class that implements a list item for the ship browser.
/// </summary>
internal class ShipBrowserListItem
{
    /// <summary>
    /// Creates an instance of the <see cref="ShipBrowserListItem"/> class with the specified parameters.
    /// </summary>
    /// <param name="sector">An <see cref="ISectorModel"/> that is the ship sector.</param>
    /// <param name="ship">An <see cref="IShipModel"/> that is the ship.</param>
    internal ShipBrowserListItem(
                                 ISectorModel sector,
                                 IShipModel ship)
        : this(
               string.IsNullOrWhiteSpace(sector.Name) ? "Unknown Sector" : sector.Name,
               ship)
    {
        Sector = sector;
    }

    /// <summary>
    /// Creates an instance of the <see cref="ShipBrowserListItem"/> class with the specified parameters.
    /// </summary>
    /// <param name="location">A <see cref="string"/> containing the ship location.</param>
    /// <param name="ship">An <see cref="IShipModel"/> that is the ship.</param>
    internal ShipBrowserListItem(
                                 string location,
                                 IShipModel ship)
    {
        Location = location;
        Ship = ship;

        Cargo = GetCargo(ship);
        Class = GetClass(ship);
        HasCargo = ship.Cargo.Any();
        HasModifications = GetHasModifications(ship);
        Modifications = GetModifications(ship);
        Name = ship.Name ?? ship.Model;
        OwnerAcronym = string.IsNullOrWhiteSpace(Ship.Owner.Acronym) ? Ship.Owner.Name : Ship.Owner.Acronym;
        X = $"{ship.Transform.Position.X:0}";
        Y = $"{ship.Transform.Position.Y:0}";
        Z = $"{ship.Transform.Position.Z:0}";
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

            if (count == 1)
            {
                var ware = cargoItems.First().Name;
                return $"{totalAmount:#,##0} ({ware})";
            }
            else
            {
                return $"{totalAmount:#,##0} ({count} wares)";
            }
        }

        return string.Empty;
    }

    private static string GetClass(IShipModel source)
        => source.Class switch
        {
            ShipClass.ExtraLarge => Constants.ShipClass.ExtraLarge,
            ShipClass.ExtraSmall => Constants.ShipClass.ExtraSmall,
            ShipClass.Large => Constants.ShipClass.Large,
            ShipClass.Medium => Constants.ShipClass.Medium,
            ShipClass.Small => Constants.ShipClass.Small,
            _ => "Unknown"
        };

    private static bool GetHasModifications(IShipModel source, bool includePaintModification = false)
        => source.EngineModification is not null ||
           source.ShieldModification is not null ||
           source.ShipModification is not null ||
           source.WeaponModifications.Any() ||
           includePaintModification &&
           source.PaintModification is not null;

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

    private const string ModificationBasic = "〉";
    private const string ModificationEnhanced = "〉〉";
    private const string ModificationExceptional = "〉〉〉";

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
            returnValue += ModificationExceptional;

            if (exceptionModifications > 1)
            {
                returnValue += $" ({exceptionModifications})";
            }

            returnValue += ", ";
        }

        if (enhancedModifications > 0)
        {
            returnValue += ModificationEnhanced;

            if (enhancedModifications > 1)
            {
                returnValue += $" ({enhancedModifications})";
            }

            returnValue += ", ";
        }

        if (basicModifications > 0)
        {
            returnValue += ModificationBasic;

            if (basicModifications > 1)
            {
                returnValue += $" ({basicModifications})";
            }
        }

        return returnValue.Trim(' ', ',');
    }

    /// <summary>
    /// Gets an indication of whether the ship can be captured.
    /// </summary>
    internal bool CanBeCaptured => Ship.CanBeCaptured;

    /// <summary>
    /// Gets the cargo of the ship.
    /// </summary>
    internal string Cargo { get; private set; }

    /// <summary>
    /// Gets the class of the ship.
    /// </summary>
    internal string Class { get; private set; }

    /// <summary>
    /// Gets an indication of whether the ship has any cargo.
    /// </summary>
    internal bool HasCargo { get; private set; }

    /// <summary>
    /// Gets an indication of whether the ship has any modifications.
    /// </summary>
    internal bool HasModifications { get; private set; }

    /// <summary>
    /// Gets the ship location.
    /// </summary>
    internal string Location { get; private set; }

    /// <summary>
    /// Gets the ship model.
    /// </summary>
    internal string Model => Ship.Model;

    /// <summary>
    /// Gets the name of the ship.
    /// </summary>
    internal string Name { get; private set; }

    /// <summary>
    /// Gets a list of the ship modifications.
    /// </summary>
    internal string Modifications { get; private set; }

    /// <summary>
    /// Gets the owner acronym of the ship.
    /// </summary>
    internal string OwnerAcronym { get; private set; }

    /// <summary>
    /// Gets the sector the ship is in.
    /// </summary>
    internal ISectorModel? Sector { get; private set; }

    /// <summary>
    /// Gets the ship.
    /// </summary>
    internal IShipModel Ship { get; private set; }

    /// <summary>
    /// Gets the X position of the ship.
    /// </summary>
    internal string X { get; private set; }

    /// <summary>
    /// Gets the X position of the ship.
    /// </summary>
    internal string Y { get; private set; }

    /// <summary>
    /// Gets the X position of the ship.
    /// </summary>
    internal string Z { get; private set; }
}
