
using System.Collections;
using HarmonyLib;
using Ramune.MoreDecoys.Handlers;
using Ramune.MoreDecoys.Items;
using RamuneLib.Utilities;
using UnityEngine;
using UWE;

namespace Ramune.MoreDecoys
{
    [HarmonyPatch(typeof(CyclopsDecoyLauncher), nameof(CyclopsDecoyLauncher.LaunchDecoy))]
    public static class LauncherPatch
    {
        public static TechType decoy;

        public static void Prefix(CyclopsDecoyLauncher __instance)
        {
            decoy = LoadingTubePatch.DecoyToFire;
            if (decoy == TechType.CyclopsDecoy) return;
            else if (decoy == StasisDecoy.info.TechType) __instance.decoyPrefab.EnsureComponent<StasisHandler>();
            else if (decoy == ExplosiveDecoy.info.TechType) __instance.decoyPrefab.EnsureComponent<ExplosiveHandler>();
            else if (decoy == GasDecoy.info.TechType) __instance.decoyPrefab.EnsureComponent<GasHandler>();
            else MoreDecoys.logger.LogWarning($"Unknown TechType '{decoy}' fired from decoy launcher");
        }
    }


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
            for(int i = 5; i >= 1; i--)
            {
                string text = slot + i.ToString();
                TechType techType = loadingTube.decoySlots.GetTechTypeInSlot(text);
                if(techType != TechType.None) return techType;
            }
            return TechType.None;
        }
    }


    [HarmonyPatch(typeof(SubRoot), nameof(SubRoot.Awake))]
    public static class SubRootPatch
    {
        public static void Postfix(SubRoot __instance)
        {
            if(!__instance.isCyclops) return;
            CoroutineHost.StartCoroutine(WaitForButton(__instance));
        }

        public static IEnumerator WaitForButton(SubRoot subRoot)
        {
            var button = subRoot.gameObject.GetComponentInChildren<CyclopsDecoyLaunchButton>();
            while(button == null)
            {
                yield return null;
                button = subRoot.gameObject.GetComponentInChildren<CyclopsDecoyLaunchButton>();
            }
            if(button != null) button.cooldown = 3.3f;
        }
    }
}