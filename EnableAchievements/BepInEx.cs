
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using System;

namespace Ramune.EnableAchievements
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class EnableAchievements : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.EnableAchievements";
        private const string pluginName = "Enable Achievements";
        private const string versionString = "1.0.2";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Console.WriteLine($"Loaded [{pluginName} {versionString}]");
            logger = Logger;
        }
    }
}