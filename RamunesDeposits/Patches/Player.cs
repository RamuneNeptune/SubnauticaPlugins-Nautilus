


namespace Ramune.RamunesOutcrops.Patches
{
    [HarmonyPatch(typeof(Player))]
    public static class PlayerPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), nameof(Player.GetBreathPeriod))]
        public static void GetBreathPeriod(ref float __result)
        {
            //if(Inventory.Get().equipment.GetCount(RadiantRebreather.Info.TechType) > 0) __result = 4.5f;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), nameof(Player.GetOxygenPerBreath))]
        public static void GetOxygenPerBreath(Player __instance, float breathingInterval, int depthClass, ref float __result)
        {
            /*
            if(__result == 0f) return;
            if(Inventory.main.equipment.GetCount(RadiantRebreather.Info.TechType) > 0 && __instance.mode != Player.Mode.Piloting && __instance.mode != Player.Mode.LockedPiloting)
            {
                if(depthClass == 1) __result = 4.0f; else
                if(depthClass == 2) __result = 4.5f; else
                if(depthClass == 3) __result = 4.5f;
            }
            */
        }
    }
}