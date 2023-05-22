
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using Ramune.StasisRifleUpgrades.Items;
using UnityEngine;
using Sentry;

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

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            StasisRifleMK1.Patch();
            StasisRifleMK2.Patch();
            StasisRifleMK3.Patch();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
}