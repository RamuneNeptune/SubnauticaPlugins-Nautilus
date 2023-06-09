﻿
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using Nautilus.Options.Attributes;
using Nautilus.Json;
using Nautilus.Options;

namespace Ramune.CreatureReskins
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class CreatureReskins : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.CreatureReskins";
        private const string pluginName = "Creature Reskins";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
    [Menu("Creature Reskins")]
    public class Options : ConfigFile
    {
        [Button("Open retextures folder")]
        public void Open(ButtonClickedEventArgs _)
        {

        }
    }
}