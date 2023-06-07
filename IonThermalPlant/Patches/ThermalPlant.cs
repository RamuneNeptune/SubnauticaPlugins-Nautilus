
using HarmonyLib;
using RamuneLib;
using UnityEngine;

namespace Ramune.IonThermalPlant.Patches
{
    [HarmonyPatch(typeof(ThermalPlant), nameof(ThermalPlant.AddPower))]
    public static class ThermalPlantPatch
    {
        [HarmonyPrefix]
        public static bool AddPower(ThermalPlant __instance, bool __runOriginal)
        {
            if(__instance.name != "IonThermalPlant(Clone)") return true;
            if(__instance.constructable.constructed && __instance.temperature < 25f) return false;

			float num1 = 2f * DayNightCycle.main.dayNightSpeed;
			float num2 = 1.6500001f * num1 * Mathf.Clamp01(Mathf.InverseLerp(25f, 100f, __instance.temperature));
            float num3 = num2 * IonThermalPlant.config.powerMultiplier;
            __instance.powerSource.AddEnergy(num3, out float _);
            __instance.powerSource.maxPower = IonThermalPlant.config.powerMaxCapacity;

            return false;
        }
    }
}