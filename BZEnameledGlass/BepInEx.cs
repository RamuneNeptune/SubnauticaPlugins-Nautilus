

using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using Ramune.BZEnameledGlass.Items;

namespace Ramune.BZEnameledGlass
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class SeaglideUpgrades : BaseUnityPlugin
    {
        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        private const string myGUID = "com.ramune.BZEnameledGlass";
        private const string pluginName = "BZ Enameled Glass";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            AltEnameledGlass.Patch();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
}