
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using Nautilus.Handlers;
using Nautilus.Options.Attributes;
using Nautilus.Json;
using RamuneLib;

namespace Ramune.TableCoralMultiplier
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class TableCoralMultiplier : BaseUnityPlugin
    {
        internal static Options config { get; } = OptionsPanelHandler.RegisterModOptions<Options>();

        public static ManualLogSource logger;
        private static readonly Harmony harmony = new Harmony(myGUID);
        private const string myGUID = "com.ramune.TableCoralMultiplier";
        private const string pluginName = "Table Coral Multiplier";
        private const string versionString = "1.0.1";

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
    [Menu("Table Coral Multiplier")]
    public class Options : ConfigFile
    {
        [Slider("Table coral to spawn", Format = "{0:F1}", DefaultValue = 1f, Min = 1f, Max = 10f, Step = 1f, Tooltip = "Changes are applied automatically", Order = 3)]
        public float ToSpawn = 1f;

        [Slider("Table coral to spawn multiplier (x)", Format = "{0:F1}x", DefaultValue = 1f, Min = 1f, Max = 10f, Step = 1f, Tooltip = "Changes are applied automatically", Order = 4)]
        public float ToSpawnMultiplier = 1f;
    }
}