
using Nautilus.Options.Attributes;
using Nautilus.Handlers;
using BepInEx.Logging;
using Nautilus.Json;
using UnityEngine;
using HarmonyLib;
using RamuneLib;
using BepInEx;

namespace Ramune.SeaglideUpgrades
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class SeaglideUpgrades : BaseUnityPlugin
    {
        internal static Options config { get; } = OptionsPanelHandler.RegisterModOptions<Options>();

        private const string myGUID = "com.ramune.SeaglideUpgrades";
        private const string pluginName = "Seaglide Upgrades";
        private const string versionString = "1.0.1";

        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        public static Texture2D MK1_Tex = Utilities.GetTexture("MK1_Tex");
        public static Texture2D MK2_Tex = Utilities.GetTexture("MK2_Tex");
        public static Texture2D MK3_Tex = Utilities.GetTexture("MK3_Tex");
        public static Texture2D MK1_Illum = Utilities.GetTexture("MK1_Illum");
        public static Texture2D MK2_Illum = Utilities.GetTexture("MK2_Illum");
        public static Texture2D MK3_Illum = Utilities.GetTexture("MK3_Illum");

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
    [Menu("Seaglide Upgrades")]
    public class Options : ConfigFile
    {
       
    }
}