
using HarmonyLib;
using Ramune.StasisRifleUpgrades.Items;

namespace Ramune.StasisRifleUpgrades.Patches
{
    [HarmonyPatch(typeof(PlayerTool), nameof(PlayerTool.animToolName), MethodType.Getter)]
    public static class PlayerToolPatch
    {
        public static void Postfix(PlayerTool __instance, ref string __result)
        {
            if(__instance.pickupable?.GetTechType() == StasisRifleUpgrades.InfoMK1.TechType) __result = "stasisrifle";
            if(__instance.pickupable?.GetTechType() == StasisRifleUpgrades.InfoMK2.TechType) __result = "stasisrifle";
            if(__instance.pickupable?.GetTechType() == StasisRifleUpgrades.InfoMK3.TechType) __result = "stasisrifle";
        }
    }
}