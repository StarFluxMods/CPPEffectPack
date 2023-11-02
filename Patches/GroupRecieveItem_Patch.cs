using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Kitchen;

namespace CPPEffectsPack.Patches
{
    [HarmonyPatch]
    public static class GroupRecieveItem_Patch
    {
        private static MethodBase TargetMethod()
        {
            Type type = AccessTools.FirstInner(typeof(GroupReceiveItem), t => t.Name.Contains("c__DisplayClass_OnUpdate_LambdaJob0"));
            return AccessTools.FirstMethod(type, method => method.Name.Contains("OriginalLambdaBody"));
        }

        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher matcher = new(instructions);
            matcher.MatchForward(false, new CodeMatch(OpCodes.Ldfld), new CodeMatch(OpCodes.Mul), new CodeMatch(OpCodes.Stloc_S))
                .Advance(2)
                .Insert(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Utility), "ChangeMoney")));
            return matcher.InstructionEnumeration();
        }
    }
}