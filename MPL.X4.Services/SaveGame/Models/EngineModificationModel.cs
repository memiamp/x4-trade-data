namespace MPL.X4.SaveGame.Models;

/// <summary>
/// A class that implements a model of an engine modification.
/// </summary>
internal class EngineModificationModel : ModificationModel, IEngineModificationModel
{
    public required double BoostAcceleration{ get; init; }

    public required double BoostDuration{ get; init; }

    public required double BoostThrust{ get; init; }

    public required double ForwardThrust{ get; init; }

    public required double RotationThrust{ get; init; }

    public required double StrafeAcceleration{ get; init; }

    public required double StrafeThrust{ get; init; }

    public required double TravelAttackTime{ get; init; }

    public required double TravelChargeTime{ get; init; }

    public required double TravelStartThrust{ get; init; }

    public required double TravelThrust{ get; init; }

    public override ModificationType Type =>  ModificationType.Engine;
}
