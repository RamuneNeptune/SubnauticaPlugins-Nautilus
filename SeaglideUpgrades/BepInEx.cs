
using Nautilus.Options.Attributes;
using Nautilus.Handlers;
using BepInEx.Logging;
using Nautilus.Json;
using UnityEngine;
using HarmonyLib;
using RamuneLib;
using BepInEx;
using Ramune.SeaglideUpgrades.Items;

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
            Workbench();
            MK1.Patch();
            MK2.Patch();
            MK3.Patch();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }

        public void Workbench()
        {
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "LithiumIonBattery");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "HeatBlade");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "PlasteelTank");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "HighCapacityTank");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "UltraGlideFins");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "SwimChargeFins");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "RepulsionCannon");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "CyclopsHullModule2");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "CyclopsHullModule3");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "SeamothHullModule2");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "SeamothHullModule3");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "ExoHullModule2");
            CraftTreeHandler.AddTabNode(CraftTree.Type.Workbench, "Seaglide", "Seaglide", Utilities.GetSprite(TechType.Seaglide));
        }
    }
    [Menu("Seaglide Upgrades")]
    public class Options : ConfigFile
    {
       
    }
}