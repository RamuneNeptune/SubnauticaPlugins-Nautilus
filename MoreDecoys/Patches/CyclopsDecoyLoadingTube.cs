
using HarmonyLib;

namespace Ramune.MoreDecoys.Patches
{
    [HarmonyPatch(typeof(CyclopsDecoyLoadingTube), nameof(CyclopsDecoyLoadingTube.TryRemoveDecoyFromTube))]
    public static class LoadingTubePatch
    {
        public static TechType DecoyToFire;
        public static string slot = "DecoySlot";

        public static void Prefix(CyclopsDecoyLoadingTube __instance)
        {
            DecoyToFire = GetNextDecoy(__instance);
        }

        public static TechType GetNextDecoy(CyclopsDecoyLoadingTube loadingTube)
        {
            for (int i = 5; i >= 1; i--)
            {
                string text = slot + i.ToString();
                TechType techType = loadingTube.decoySlots.GetTechTypeInSlot(text);
                if (techType != TechType.None) return techType;
            }
            return TechType.None;
        }
    }
}