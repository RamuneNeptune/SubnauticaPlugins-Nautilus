
using HarmonyLib;
using UnityEngine;

namespace Ramune.TableCoralMultiplier.Patches
{
    [HarmonyPatch(typeof(SpawnOnKill), nameof(SpawnOnKill.OnKill))]
    public static class SpawnOnKillPatch
    {
        public static bool Prefix(SpawnOnKill __instance, bool __runOriginal)
        {
            if(!__instance.prefabToSpawn.name.StartsWith("JeweledDisk")) return true;
               
            int toSpawn = (int)TableCoralMultiplier.config.ToSpawn * (int)TableCoralMultiplier.config.ToSpawnMultiplier;
            for(int i = 0; i < toSpawn; i++)
            {
                var gameObject = Object.Instantiate(__instance.prefabToSpawn, __instance.transform.position, __instance.transform.rotation);
                if(__instance.randomPush)
                {
                    var rigid = gameObject.GetComponent<Rigidbody>();
                    if(rigid) rigid.AddForce(Random.onUnitSphere * 1.4f, ForceMode.Impulse);
                }
            }
            return false;
        }
    }
}