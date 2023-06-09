
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
using Nautilus.Assets;
using UWE;

namespace Ramune.StasisRifleUpgrades
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class StasisRifleUpgrades : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.StasisRifleUpgrades";
        private const string pluginName = "Stasis Rifle Upgrades";
        private const string versionString = "1.0.2";
        public static PrefabInfo InfoMK1;
        public static PrefabInfo InfoMK2;
        public static PrefabInfo InfoMK3;
        public static GameObject Explosion;

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            StartCoroutine(FetchExplosivePrefab());
            CraftTreeHandler.AddTabNode(CraftTree.Type.Workbench, "StasisRifles", "Stasis rifles", Utilities.GetSprite(TechType.StasisRifle));
            InfoMK1 = ItemHelper.CreatePrefabInfo(1);
            InfoMK2 = ItemHelper.CreatePrefabInfo(2);
            InfoMK3 = ItemHelper.CreatePrefabInfo(3);
            ItemHelper.CreateRifle(1, InfoMK1, Color.green);
            ItemHelper.CreateRifle(2, InfoMK2, Color.yellow);
            ItemHelper.CreateRifle(3, InfoMK3, Color.red);
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }

        public IEnumerator FetchExplosivePrefab()
        {
            logger.LogFatal("WAITING FOR PLAYER");
            yield return new WaitUntil(() => Player.main);

            var task = CraftData.GetPrefabForTechTypeAsync(TechType.Crash);
            yield return task;

            var result = task.GetResult();
            var crash = result.GetComponentInChildren<Crash>();
            Explosion = crash.detonateParticlePrefab;
            yield break;
        }
    }
}