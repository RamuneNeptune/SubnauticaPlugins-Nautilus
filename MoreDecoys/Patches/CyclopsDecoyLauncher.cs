
using HarmonyLib;
using Ramune.MoreDecoys.Handlers;
using Ramune.MoreDecoys.Items;

namespace Ramune.MoreDecoys.Patches
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
}