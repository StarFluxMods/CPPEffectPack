using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Kitchen;

namespace CPPEffectsPack.Patches
{
    [HarmonyPatch(typeof(ApplyItemProcesses), "Run")]
    public static class ApplyItemProcess_Patch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            object oprand = null;
            CodeMatcher matcher = new(instructions);
            matcher.MatchForward(false, new CodeMatch(OpCodes.Call), new CodeMatch(OpCodes.Ldloca_S), new CodeMatch(OpCodes.Callvirt)).Advance(1);
            oprand = matcher.Operand;
            matcher.MatchForward(false, new CodeMatch(OpCodes.Mul), new CodeMatch(OpCodes.Stloc_S), new CodeMatch(OpCodes.Ldloc_S))
                .Insert(new CodeInstruction(OpCodes.Ldloc_S, oprand), new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Utility), "GetCookSpeed")));
            return matcher.InstructionEnumeration();
        }
    }
}