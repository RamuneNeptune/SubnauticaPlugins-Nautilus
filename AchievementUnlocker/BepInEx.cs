
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using System;

namespace Ramune.AchievementUnlocker
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class AchievementUnlocker : BaseUnityPlugin
    {
        private const string myGUID = "com.ramune.AchievementUnlocker";
        private const string pluginName = "Achievement Unlocker";
        private const string versionString = "1.0.0";

        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Console.WriteLine($"Loaded [{pluginName} {versionString}]");
            logger = Logger;
        }
    }
}