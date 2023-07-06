
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using Nautilus.Options.Attributes;
using Nautilus.Json;
using UnityEngine;
using Nautilus.Handlers;
using System;

namespace Ramune.PrawnSuitLightSwitch
{
    [BepInDependency("com.snmodding.nautilus")]
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
            Main.FindPiracy();
            Console.WriteLine($"Loaded [{pluginName} {versionString}]");
            logger = Logger;
        }
    }
    [Menu("Prawn Suit Light Switch")]
    public class Options : ConfigFile
    {
        [Keybind("Toggle lights key")]
        public KeyCode toggle = KeyCode.Mouse2;

        [Toggle("Enable toggle on/off sounds")]
        public bool sounds = true;

        [Toggle("Enable toggle on/off subtitles")]
        public bool debug = false;
    }
}