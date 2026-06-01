using MPL.X4.Services;

namespace MPL.X4.SaveGame.Data;

/// <summary>
/// A class that implements a data model of an engine modification.
/// </summary>
internal class EngineModificationData : ModificationData, IEngineModificationData
{
    public override string ToString()
        => $"{Ware} - {DebugOutputHelper.GetPropertyValues<double>(this, x => x != 0)}";

    public required double BoostAcceleration { get; init; }

    public required double BoostDuration { get; init; }

    public required double BoostThrust { get; init; }

    public required double ForwardThrust { get; init; }

    public required double RotationThrust { get; init; }

    public required double StrafeAcceleration { get; init; }

    public required double StrafeThrust { get; init; }

    public required double TravelAttackTime { get; init; }

    public required double TravelChargeTime { get; init; }

    public required double TravelStartThrust { get; init; }

    public required double TravelThrust { get; init; }
}
