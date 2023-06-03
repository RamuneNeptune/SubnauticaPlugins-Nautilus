
using HarmonyLib;
using Ramune.RamunesOutcrops.Items;

namespace Ramune.RamunesOutcrops.Patches
{
    [HarmonyPatch(typeof(Player))]
    public static class PlayerPatches
    {
        [HarmonyPatch(typeof(Player), nameof(Player.GetBreathPeriod))]
        public static void GetBreathPeriod(ref float __result)
        {
            if(Inventory.Get().equipment.GetCount(RadiantRebreather.Info.TechType) > 0) __result = 4f;
        }

        [HarmonyPatch(typeof(Player), nameof(Player.GetOxygenPerBreath))]
        public static void GetOxygenPerBreath(Player __instance, float breathingInterval, int depthClass, ref float __result)
        {
            ErrorMessage.AddError($"{breathingInterval}, {__result}");
            if(Inventory.main.equipment.GetCount(RadiantRebreather.Info.TechType) > 0 && __instance.mode != Player.Mode.Piloting && __instance.mode != Player.Mode.LockedPiloting)
            {
                if(depthClass == 2) __result = 2.5f;
                else if(depthClass == 3) __result = 3.5f;
            }
        }
    }
}