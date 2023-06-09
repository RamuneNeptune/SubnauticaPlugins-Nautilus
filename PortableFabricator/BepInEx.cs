
global using Nautilus.Assets.PrefabTemplates;
global using UnityEngine.AddressableAssets;
global using System.Collections.Generic;
global using Nautilus.Assets.Gadgets;
global using Nautilus.Extensions;
global using System.Collections;
global using Nautilus.Handlers;
global using Nautilus.Crafting;
global using Nautilus.Utility;
global using static CraftData;
global using BepInEx.Logging;
global using Nautilus.Assets;
global using UnityEngine;
global using System.Linq;
global using HarmonyLib;
global using RamuneLib;
global using BepInEx;
global using System;


namespace Ramune.PortableFabricator
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class PortableFabricator : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.PortableFabricator";
        private const string pluginName = "Portable Fabricator";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            logger = Logger;
            harmony.PatchAll();
            Main.FindPiracy();
            Buildables.PortableFabricator.Patch();
            Craftables.PortableFabricatorReciever.Patch();
        }
    }
}