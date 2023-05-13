﻿
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib.Main;
using Nautilus.Options.Attributes;
using Nautilus.Json;
using UnityEngine;
using Nautilus.Handlers;

namespace Ramune.PrawnSuitLightSwitch
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class PrawnSuitLightSwitch : BaseUnityPlugin
    {
        internal static Options config { get; } = OptionsPanelHandler.RegisterModOptions<Options>();

        private const string myGUID = "com.ramune.PrawnSuitLightSwitch";
        private const string pluginName = "Prawn Suit Light Switch";
        private const string versionString = "1.0.0";

        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        public void Awake()
        {
            harmony.PatchAll();
            Checks.FindPiracy();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
    [Menu("Prawn Suit Light Switch")]
    public class Options : ConfigFile
    {
        [Keybind("Toggle lights key")]
        public KeyCode toggle = KeyCode.Mouse2;

        [Toggle("Toggle on/off sounds")]
        public bool sounds = true;

        [Toggle("Toggle on/off subtitles")]
        public bool debug = false;
    }
}