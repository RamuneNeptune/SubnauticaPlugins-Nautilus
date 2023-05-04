
using HarmonyLib;
using Ramune.MoreDecoys.Items;
using UnityEngine;

namespace Ramune.MoreDecoys
{
    [HarmonyPatch(typeof(CyclopsDecoyLauncher), nameof(CyclopsDecoyLauncher.LaunchDecoy))]
    public static class LauncherPatch
    {
        public static DecoyHandler handler;
        public static TechType decoy;
        public static void Prefix(CyclopsDecoyLauncher __instance)
        {
            decoy = LoadingTubePatch.DecoyToFire;
            if(decoy == TechType.CyclopsDecoy) return;
            else if(decoy == StasisDecoy.info.TechType) __instance.decoyPrefab.EnsureComponent<DecoyHandler>().id = 1;
            else if(decoy == ExplosiveDecoy.info.TechType) __instance.decoyPrefab.EnsureComponent<DecoyHandler>().id = 2;
            else if(decoy == GasDecoy.info.TechType) __instance.decoyPrefab.EnsureComponent<DecoyHandler>().id = 3;
            else MoreDecoys.logger.LogWarning($"Unknown TechType '{decoy}' fired from decoy launcher");
        }
    }


    [HarmonyPatch(typeof(CyclopsDecoyLoadingTube), nameof(CyclopsDecoyLoadingTube.TryRemoveDecoyFromTube))]
    public static class LoadingTubePatch
    {
        public static TechType DecoyToFire; 
        public static void Prefix(CyclopsDecoyLoadingTube __instance)
        {
            DecoyToFire = GetNextDecoy(__instance);
        }

        public static TechType GetNextDecoy(CyclopsDecoyLoadingTube loadingTube)
        {
            for (int i = 5; i >= 1; i--) 
            {
                string text = "DecoySlot" + i.ToString();
                TechType techType = loadingTube.decoySlots.GetTechTypeInSlot(text);
                if (techType != TechType.None) return techType; 
            }
            return TechType.None; 
        }
    }

    /*
    [HarmonyPatch(typeof(PrecursorGunAim), nameof(PrecursorGunAim.LateUpdate))]
    public static class PrecursorGunAimPatch
    {
        public static bool Prefix(PrecursorGunAim __instance, bool __runOriginal)
        {
            if(!__instance.target) return false;
            Quaternion rhs = Quaternion.LookRotation(Player.main.transform.position - __instance.gunArm.position, Vector3.up);
            Quaternion b = Quaternion.Inverse(__instance.gunBase.parent.rotation) * rhs;
            Quaternion quaternion = Quaternion.Slerp(__instance.oldBaseRot, b, Time.deltaTime / __instance.damper);
            __instance.gunBase.localRotation = Quaternion.Euler(0f, quaternion.eulerAngles.y, 0f);
            __instance.oldBaseRot = __instance.gunBase.localRotation;
            Quaternion b2 = Quaternion.Inverse(__instance.gunArm.parent.rotation) * rhs;
            quaternion = Quaternion.Slerp(__instance.oldBaseRot, b2, Time.deltaTime / __instance.damper);
            __instance.gunArm.localRotation = Quaternion.Euler(quaternion.eulerAngles.x, 0f, 0f);
            __instance.oldArmRot = __instance.gunArm.localRotation;
            return false;
        }
    }
    */
}