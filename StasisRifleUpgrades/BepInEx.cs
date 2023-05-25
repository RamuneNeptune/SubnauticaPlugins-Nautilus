
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using Ramune.StasisRifleUpgrades.Items;
using UnityEngine;
using Sentry;
using System.Collections;
using System.Collections.Generic;
using Nautilus.Handlers;

namespace Ramune.StasisRifleUpgrades
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class StasisRifleUpgrades : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.StasisRifleUpgrades";
        private const string pluginName = "Stasis Rifle Upgrades";
        private const string versionString = "1.0.1";
        public static GameObject Explosion;

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            StartCoroutine(FetchExplosivePrefab());
            StasisRifleMK1.Patch();
            StasisRifleMK2.Patch();
            StasisRifleMK3.Patch();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }

        public IEnumerator FetchExplosivePrefab()
        {
            yield return new WaitUntil(() => Player.main);
            logger.LogFatal("I am fetching the explosive prefab");
            var prefab = CraftData.GetPrefabForTechTypeAsync(TechType.Crash);
            yield return prefab;
            Explosion = prefab.GetResult().GetComponentInChildren<Crash>().detonateParticlePrefab;
            logger.LogFatal("I got the prefab, I mean come on, did you really expect me to let you down?");
            yield break;
        }
    }
}