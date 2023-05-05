
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib.Main;
using Nautilus.Options.Attributes;
using Nautilus.Json;
using Nautilus.Handlers;

namespace Ramune.FasterScanning
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class FasterScanning : BaseUnityPlugin
    {
        internal static Options config { get; } = OptionsPanelHandler.RegisterModOptions<Options>();

        private const string myGUID = "com.ramune.FasterScanning";
        private const string pluginName = "Faster Scanning";
        private const string versionString = "1.0.2";

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
    [Menu("Faster Scanning")]
    public class Options : ConfigFile
    {
        [Slider("Scanning speed multiplier", Format = "{0:0.0}x", DefaultValue = 1f, Min = 0.1f, Max = 5f, Step = 0.1f, Tooltip = "E.g. setting this to '2', would be double scanning speed")]
        public float ScanSpeed = 1f;
    }
}