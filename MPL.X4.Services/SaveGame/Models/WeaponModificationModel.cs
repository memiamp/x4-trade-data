namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of a weapon modification.
/// </summary>
internal class WeaponModificationModel : ModificationModel, IWeaponModificationModel
{
    public required double ChargeTime{ get; init; }

    public required double Cooling{ get; init; }

    public required double Damage{ get; init; }

    public required double Lifetime{ get; init; }

    public required double Mining{ get; init; }

    public required double Reload{ get; init; }

    public required double RotationSpeed{ get; init; }

    public required double StickTime{ get; init; }

    public required double SurfaceElement{ get; init; }

    public override ModificationType Type => ModificationType.Weapon;
}
