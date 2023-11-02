using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Kitchen;

namespace CPPEffectsPack.Patches
{
    [HarmonyPatch(typeof(GrabItems), "OnUpdate")]
    public static class GrabItems_Patch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher matcher = new(instructions);
            matcher.MatchForward(false, new CodeMatch(OpCodes.Brtrue), new CodeMatch(OpCodes.Ldc_R4), new CodeMatch(OpCodes.Br))
                .Advance(1)
                .Set(OpCodes.Call, AccessTools.Method(typeof(Utility), "GetGrabberSpeed"))
                .MatchForward(false, new CodeMatch(OpCodes.Br), new CodeMatch(OpCodes.Ldc_R4), new CodeMatch(OpCodes.Stfld))
                .Advance(1)
                .Set(OpCodes.Call, AccessTools.Method(typeof(Utility), "GetGrabberSpeedWithHalloweenCard"));
            return matcher.InstructionEnumeration();
        }
    }
}