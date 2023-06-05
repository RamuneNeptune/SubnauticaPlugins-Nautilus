


namespace Ramune.RamunesOutcrops.Patches
{
    [HarmonyPatch(typeof(PlayerTool), nameof(PlayerTool.animToolName), MethodType.Getter)]
    public static class PlayerToolPatch
    {
        public static void Postfix(PlayerTool __instance, ref string __result)
        {
            if(__instance.pickupable?.GetTechType() == RadiantSeaglide.Info.TechType) __result = "seaglide";
            if(__instance.pickupable?.GetTechType() == RadiantThermoblade.Info.TechType) __result = "knife";
        }
    }
}