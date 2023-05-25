
using HarmonyLib;
using RamuneLib;
using UnityEngine;

namespace Ramune.IonThermalPlant.Patches
{
    [HarmonyPatch(typeof(ThermalPlant), nameof(ThermalPlant.AddPower))]
    public static class ThermalPlantPatch
    {
        public static void Postfix(ThermalPlant __instance, bool __runOriginal)
        {
            if(__instance.name != "IonThermalPlant(Clone)") return; 

            if(__instance.constructable.constructed && __instance.temperature > 25f)
            {
                float speed = 2f * DayNightCycle.main.dayNightSpeed;
                float amount = 4f * speed * Mathf.Clamp01(Mathf.InverseLerp(25f, 100f, __instance.temperature));
                float amountStored = 0f;
                __instance.powerSource.AddEnergy(amount, out amountStored);
            }
        }
    }
}