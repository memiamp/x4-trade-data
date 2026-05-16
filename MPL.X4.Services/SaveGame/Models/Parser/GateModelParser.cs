using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.SaveGame.Data;

namespace MPL.X4.SaveGame.Models.Parser;

/// <summary>
/// A class that implements a parser to a <see cref="IGateModel"/> from an <see cref="IGateData"/>.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="parsingScope">An <see cref="ISaveGameModelParsingScope"/> that is the parsing scope.</param>
internal class GateModelParser(
                               ILogger<GateModelParser> logger,
                               ISaveGameModelParsingScope parsingScope)
    : ModelParserBase<IGateData, IGateModel>(logger)
{
    private static readonly Dictionary<string, GateType> _typeMap = new()
    {
        { "props_anc_gate_01_blocked_macro", GateType.JumpGate },
        { "props_gates_anc_gate_anim_macro", GateType.JumpGate },
        { "props_gates_anc_gate_macro", GateType.JumpGate },
        { "props_ter_gate_01_macro", GateType.JumpGate },
        { "props_ter_gate_02_macro", GateType.JumpGate },

        { "props_gates_orb_accelerator_01_macro", GateType.TransorbitalAccelerator },
        { "props_gates_orb_accelerator_02_macro", GateType.TransorbitalAccelerator },
        { "props_ter_accelerator_01_macro", GateType.TransorbitalAccelerator },

        { "props_ter_gate_01_destroyed_macro", GateType.Unknown }
    };

    private protected override IGateModel OnParse(IGateData source)
    {
        var type = ParseGateType(source.Macro);
        var transform = source.Transform.Add(parsingScope.CurrentOffset);

        return new GateModel
        {
            Code = source.Code,
            IsKnown = source.IsKnown,
            Id = source.Id,
            Transform = transform,
            Type = type
        };
    }

    private GateType ParseGateType(string macro)
    {
        if (!_typeMap.TryGetValue(macro, out var returnValue))
        {
            Logger.LogWarning("Unable to map gate macro {Macro} to a type", macro);
        
            returnValue = GateType.Unknown;
        }

        return returnValue;
    }
}
