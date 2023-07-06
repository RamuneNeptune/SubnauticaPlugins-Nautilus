
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using Nautilus.Options.Attributes;
using Nautilus.Handlers;
using Nautilus.Json;
using Nautilus.Options;
using System;

namespace Ramune.IonThermalPlant
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class IonThermalPlant : BaseUnityPlugin
    {
        internal static Options config { get; } = OptionsPanelHandler.RegisterModOptions<Options>();

        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.IonThermalPlant";
        private const string pluginName = "Ion Thermal Plant";
        private const string versionString = "1.0.0";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Items.IonThermalPlant.Patch();
            Console.WriteLine($"Loaded [{pluginName} {versionString}]");
            logger = Logger;
        }
    }
    [Menu("Ion Thermal Plant")]
    public class Options : ConfigFile
    {
        [Slider("Power generation multiplier (x)", Format = "{0:F1}x", DefaultValue = 2.2f, Min = 1f, Max = 10f, Step = 0.1f, Tooltip = "Power generation will be multiplied by this amount. (Default: 2.2x)")]
        public float powerMultiplier;

        [Slider("Maximum power capacity", Format = "{0:F0}", DefaultValue = 500f, Min = 1f, Max = 1000f, Step = 1f, Tooltip = "The maximum amount of power the thermal plant can store. (Default: 500)")]
        public float powerMaxCapacity;

        [Button("Unlock Ion thermal plant")]
        public void Do(ButtonClickedEventArgs _)
        {
            if(!KnownTech.Contains(Items.IonThermalPlant.Info.TechType))
            {
                KnownTech.Add(Items.IonThermalPlant.Info.TechType);
            }
        }
    }
}