
using HarmonyLib;
using RamuneLib;
using UnityEngine;

namespace Ramune.IonSolarPanel.Patches
{
    [HarmonyPatch(typeof(SolarPanel))]
    public static class SolarPanelPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(SolarPanel), nameof(SolarPanel.Start))]
        public static void Start(SolarPanel __instance)
        {
            if(__instance.name != "IonSolarPanel(Clone)") return;

            var solar = __instance.GetComponent<SolarPanel>();
            solar.maxDepth = 500;

            var power = __instance.GetComponent<PowerSource>();
            power.maxPower = 200;
        }
    }
}