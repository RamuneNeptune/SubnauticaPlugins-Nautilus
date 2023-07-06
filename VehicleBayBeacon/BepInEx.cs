
using BepInEx.Logging;
using UnityEngine;
using HarmonyLib;
using RamuneLib;
using BepInEx;
using System;

namespace Ramune.VehicleBayBeacon
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class VehicleBayBeacon : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.VehicleBayBeacon";
        private const string pluginName = "Vehicle Bay Beacon";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Console.WriteLine($"Loaded [{pluginName} {versionString}]");
            logger = Logger;
        }
    }
}