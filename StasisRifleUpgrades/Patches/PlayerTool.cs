
using HarmonyLib;
using Ramune.StasisRifleUpgrades.Items;

namespace Ramune.StasisRifleUpgrades.Patches
{
    [HarmonyPatch(typeof(PlayerTool), nameof(PlayerTool.animToolName), MethodType.Getter)]
    public static class PlayerToolPatch
    {
        public static void Postfix(PlayerTool __instance, ref string __result)
        {
            if(__instance.pickupable?.GetTechType() == StasisRifleMK1.Info.TechType) __result = "stasisrifle";
            if(__instance.pickupable?.GetTechType() == StasisRifleMK2.Info.TechType) __result = "stasisrifle";
            if(__instance.pickupable?.GetTechType() == StasisRifleMK3.Info.TechType) __result = "stasisrifle";
        }
    }
}