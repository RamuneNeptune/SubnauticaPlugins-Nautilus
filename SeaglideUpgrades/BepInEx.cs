
using Nautilus.Options.Attributes;
using Nautilus.Handlers;
using BepInEx.Logging;
using Nautilus.Json;
using UnityEngine;
using HarmonyLib;
using RamuneLib;
using BepInEx;
using Ramune.SeaglideUpgrades.Items;
using Nautilus.Options;

namespace Ramune.SeaglideUpgrades
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class SeaglideUpgrades : BaseUnityPlugin
    {
        internal static Options config { get; } = OptionsPanelHandler.RegisterModOptions<Options>();
        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        private const string myGUID = "com.ramune.SeaglideUpgrades";
        private const string pluginName = "Seaglide Upgrades";
        private const string versionString = "1.0.1";

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
        [Toggle("<b>Speed multipliers</b>: <alpha=#00>---------------------------------------------------------------------------------------------------</alpha>")]
        public bool speedDivider = false;
        [Slider("   - <color=#3fb4ff>MK1</color> Speed Multiplier", Format = "{0:F1}x", DefaultValue = 1f, Min = 0.1f, Max = 5f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float speedmk1 = 1f;
        [Slider("   - <color=#c6ff53>MK2</color> Speed Multiplier", Format = "{0:F1}x", DefaultValue = 1f, Min = 0.1f, Max = 5f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float speedmk2 = 1f;
        [Slider("   - <color=#ff5733>MK3</color> Speed Multiplier", Format = "{0:F1}x", DefaultValue = 1f, Min = 0.1f, Max = 5f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float speedmk3 = 1f;
            
        [Toggle("<alpha=#00>---------------------------------------------------------------------------------------------------</alpha>")]
        public bool categoryDivider = false;

        [Toggle("<b>Enable <color=#3fb4ff>MK1</color> light settings?</b>")]
        public bool boolmk1 = false;
        [Slider("   - <color=#3fb4ff>MK1</color> Light Red (<color=#3fb4ff>R</color>)", Format = "{0:F1}", DefaultValue = 0f, Min = 0f, Max = 1f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float redmk1 = 0f;
        [Slider("   - <color=#3fb4ff>MK1</color> Light Green (<color=#3fb4ff>G</color>)", Format = "{0:F1}", DefaultValue = 0.8f, Min = 0f, Max = 1f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float greenmk1 = 0.8f;
        [Slider("   - <color=#3fb4ff>MK1</color> Light Blue (<color=#3fb4ff>B</color>)", Format = "{0:F1}", DefaultValue = 1f, Min = 0f, Max = 1f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float bluemk1 = 1f;
        [Slider("   - <color=#3fb4ff>MK1</color> Light Range", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 5f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float rangemk1 = 1f;
        [Slider("   - <color=#3fb4ff>MK1</color> Light Intensity", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 5f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float intensitymk1 = 1f;
        [Slider("   - <color=#3fb4ff>MK1</color> Light Cone Size", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 5f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float conesizemk1 = 1f;
        [Button("Display custom color on screen")]
        public void MK1(ButtonClickedEventArgs _)
        {
            Color color = new Color(redmk1, greenmk1, bluemk1);
            string hex = ColorUtility.ToHtmlStringRGBA(color);
            ErrorMessage.AddError("<color=#" + hex + ">This is an example of your chosen color</color>");
        }

        [Toggle("<b>Enable <color=#c6ff53>MK2</color> light settings?</b>")]
        public bool boolmk2 = false;
        [Slider("   - <color=#c6ff53>MK2</color> Light Red (<color=#c6ff53>R</color>)", Format = "{0:F1}", DefaultValue = 0f, Min = 0f, Max = 1f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float redmk2 = 0f;
        [Slider("   - <color=#c6ff53>MK2</color> Light Green (<color=#c6ff53>G</color>)", Format = "{0:F1}", DefaultValue = 0.8f, Min = 0f, Max = 1f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float greenmk2 = 0.8f;
        [Slider("   - <color=#c6ff53>MK2</color> Light Blue (<color=#c6ff53>B</color>)", Format = "{0:F1}", DefaultValue = 1f, Min = 0f, Max = 1f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float bluemk2 = 1f;
        [Slider("   - <color=#c6ff53>MK2</color> Light Range", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 5f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float rangemk2 = 1f;
        [Slider("   - <color=#c6ff53>MK2</color> Light Intensity", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 5f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float intensitymk2 = 1f;
        [Slider("   - <color=#c6ff53>MK2</color> Light Cone Size", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 5f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float conesizemk2 = 1f;
        [Button("Display custom color on screen")]
        public void MK2(ButtonClickedEventArgs _)
        {
            Color color = new Color(redmk2, greenmk2, bluemk2);
            string hex = ColorUtility.ToHtmlStringRGBA(color);
            ErrorMessage.AddError("<color=#" + hex + ">This is an example of your chosen color</color>");
        }

        [Toggle("<b>Enable <color=#ff5733>MK3</color> light settings?</b>")]
        public bool boolmk3 = false;
        [Slider("   - <color=#ff5733>MK3</color> Light Red (<color=#ff5733>R</color>)", Format = "{0:F1}", DefaultValue = 0f, Min = 0f, Max = 1f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float redmk3 = 0f;
        [Slider("   - <color=#ff5733>MK3</color> Light Green (<color=#ff5733>G</color>)", Format = "{0:F1}", DefaultValue = 0.8f, Min = 0f, Max = 1f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float greenmk3 = 0.8f;
        [Slider("   - <color=#ff5733>MK3</color> Light Blue (<color=#ff5733>B</color>)", Format = "{0:F1}", DefaultValue = 1f, Min = 0f, Max = 1f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float bluemk3 = 1f;
        [Slider("   - <color=#ff5733>MK3</color> Light Range", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 5f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float rangemk3 = 1f;
        [Slider("   - <color=#ff5733>MK3</color> Light Intensity", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 5f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float intensitymk3 = 1f;
        [Slider("   - <color=#ff5733>MK3</color> Light Cone Size", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 5f, Step = 0.1f, Tooltip = "Re-equip Seaglide to apply changes")]
        public float conesizemk3 = 1f;
        [Button("Display custom color on screen")]
        public void MK3(ButtonClickedEventArgs _)
        {
            Color color = new Color(redmk3, greenmk3, bluemk3);
            string hex = ColorUtility.ToHtmlStringRGBA(color);
            ErrorMessage.AddError("<color=#" + hex + ">This is an example of your chosen color</color>");
        }
    }
}