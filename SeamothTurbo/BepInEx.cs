
global using Nautilus.Assets.PrefabTemplates;
global using System.Collections.Generic;
global using Nautilus.Assets.Gadgets;
global using System.Collections;
global using Nautilus.Handlers;
global using Nautilus.Crafting;
global using Nautilus.Utility;
global using static CraftData;
global using BepInEx.Logging;
global using Nautilus.Assets;
global using UnityEngine;
global using HarmonyLib;
global using RamuneLib;
global using BepInEx;
global using System;
global using UWE;


namespace Ramune.SeamothTurbo
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class SeamothTurbo : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.SeamothTurboModule";
        private const string pluginName = "Seamoth Turbo Module";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Items.TurboModule.Patch();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
}