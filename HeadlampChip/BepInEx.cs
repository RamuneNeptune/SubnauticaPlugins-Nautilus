
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib;
using Nautilus.Handlers;
using Nautilus.Options.Attributes;
using Nautilus.Options;
using UnityEngine;
using Nautilus.Json;
using UWE;

namespace Ramune.HeadlampChip
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class HeadlampChip : BaseUnityPlugin
    {
        internal static Options config { get; } = OptionsPanelHandler.RegisterModOptions<Options>();

        private const string myGUID = "com.ramune.HeadlampChip";
        private const string pluginName = "Headlamp Chip";
        private const string versionString = "1.0.0";
        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            HeadlampChipItem.Patch();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
    [Menu("Headlamp Chip")]
    public class Options : ConfigFile
    {
        [Toggle("<b>Color settings</b>: <alpha=#00>---------------------------------------------------------------------------------------------------</alpha>")]
        public bool DIVIDER_1 = false;

        [Slider("  - <color=#FFC029>Headlamp</color> Light Red (<color=#FFC029>R</color>)", Format = "{0:F1}", DefaultValue = 1f, Min = 0f, Max = 1f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float red = 1f;
        [Slider("  - <color=#FFC029>Headlamp</color> Light Green (<color=#FFC029>G</color>)", Format = "{0:F1}", DefaultValue = 1f, Min = 0f, Max = 1f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float green = 1f;
        [Slider("  - <color=#FFC029>Headlamp</color> Light Blue (<color=#FFC029>B</color>)", Format = "{0:F1}", DefaultValue = 1f, Min = 0f, Max = 1f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float blue = 1f;

        [Toggle("<b>Light settings</b>: <alpha=#00>---------------------------------------------------------------------------------------------------</alpha>")]
        public bool DIVIDER_2 = false;

        [Slider("  - <color=#FFC029>Headlamp</color> Light Range", Format = "{0:F1}x", DefaultValue = 1f, Min = 0.1f, Max = 5f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float range = 1f;
        [Slider("  - <color=#FFC029>Headlamp</color> Light Intensity", Format = "{0:F1}x", DefaultValue = 1f, Min = 0.1f, Max = 5f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float intensity = 1f;
        [Slider("  - <color=#FFC029>Headlamp</color> Light Conesize", Format = "{0:F2}x", DefaultValue = 1f, Min = 0.1f, Max = 2f, Step = 0.01f), OnChange(nameof(Refresh))]
        public float conesize = 1f;
        [Keybind("  - <color=#FFC029>Headlamp</color> Toggle Light Key")]
        public KeyCode toggle = KeyCode.F;

        [Toggle("<b>Rainbow settings</b>: <alpha=#00>---------------------------------------------------------------------------------------------------</alpha>")]
        public bool DIVIDER_3 = false;

        [Toggle("  - <color=#FF69B4>Rainbow</color> Mode"), OnChange(nameof(Refresh))]
        public bool rainbow = false;
        [Slider("  - <color=#FF69B4>Rainbow</color> Opacity", Format = "{0:F1}x", DefaultValue = 1f, Min = 0.1f, Max = 1f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float rainbowOpacity = 1f;
        [Slider("  - <color=#FF69B4>Rainbow</color> Saturation", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 1f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float rainbowSaturation = 1f;
        [Slider("  - <color=#FF69B4>Rainbow</color> Loop Duration", Format = "{0:F1}s", DefaultValue = 10f, Min = 0.1f, Max = 20f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float rainbowDuration = 10f;

        public void Refresh(SliderChangedEventArgs _)
        {
            foreach(HeadlampChipMono Mono in HeadlampChipMono.Headlamps) Mono.Refresh();
        }
    }
}