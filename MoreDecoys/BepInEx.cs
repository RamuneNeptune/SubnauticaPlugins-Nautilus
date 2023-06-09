﻿
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using Ramune.MoreDecoys.Items;
using System;

namespace Ramune.MoreDecoys
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class MoreDecoys : BaseUnityPlugin
    {
        private const string myGUID = "com.ramune.MoreDecoys";
        private const string pluginName = "More Decoys";
        private const string versionString = "1.0.0";
        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            StasisDecoy.Patch();
            ExplosiveDecoy.Patch();
            GasDecoy.Patch();
            Console.WriteLine($"Loaded [{pluginName} {versionString}]");
            logger = Logger;
        }
    }
}