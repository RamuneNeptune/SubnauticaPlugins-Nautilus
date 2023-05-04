
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
            decoy = LoadingTubePatch.DecoyToFire; // As I've done before, this is just so I can write 'decoy' rather than the full 'LoadingTubePatch.DecoyToFire'
            if (decoy == TechType.CyclopsDecoy) return; // Return as early as possible if it's just a regular decoy

            // CyclopsDecoyLauncher.decoyPrefab is a reference we can use to get the current decoy thats going to be fired
            else if (decoy == StasisDecoy.info.TechType) __instance.decoyPrefab.EnsureComponent<DecoyHandler>().id = 1;
            else if (decoy == ExplosiveDecoy.info.TechType) __instance.decoyPrefab.EnsureComponent<DecoyHandler>().id = 2;
            // DecoyHandler is added to the decoyPrefab with an id (to know which decoy type it is), then DecoyHandler handles the stasis/explosions/etc

            else MoreDecoys.logger.LogWarning($"Unknown TechType '{decoy}' fired from decoy launcher"); // Logs if it was an unknown decoy TechType
        }
    }


    [HarmonyPatch(typeof(CyclopsDecoyLoadingTube), nameof(CyclopsDecoyLoadingTube.TryRemoveDecoyFromTube))]
    public static class LoadingTubePatch
    {
        public static TechType DecoyToFire; // Store a 'public static' TechType to be set, so it can be accessed in other places
        public static void Prefix(CyclopsDecoyLoadingTube __instance)
        {
            // Every time the game attempts to remove a decoy from the loading tube, we update the DecoyToFire
            DecoyToFire = GetNextDecoy(__instance);
        }

        // I basically just ripped this from the games code
        public static TechType GetNextDecoy(CyclopsDecoyLoadingTube loadingTube)
        {
            for (int i = 5; i >= 1; i--) // Start at 5, make sure int is over 1, takeaway 1 from int each time  ||   i.e. looping through all slots
            {
                string text = "DecoySlot" + i.ToString();
                TechType techType = loadingTube.decoySlots.GetTechTypeInSlot(text);
                if (techType != TechType.None) return techType; // Returns the decoys TechType
            }
            return TechType.None; // Returns nothing because there is nothing loaded
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