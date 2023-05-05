
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using RamuneLib.Main;
using Nautilus.Handlers;
using Nautilus.Options.Attributes;
using Nautilus.Options;
using UnityEngine;
using Nautilus.Json;

namespace Ramune.HeadlampChip
{
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
            Checks.FindPiracy();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }
    }
    [Menu("Headlamp Chip")]
    public class Options : ConfigFile
    {
        [Slider("<color=#FFC029>Headlamp</color> Light Red (<color=#FFC029>R</color>)", Format = "{0:F1}", DefaultValue = 1f, Min = 0f, Max = 1f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float red = 1f;
        [Slider("<color=#FFC029>Headlamp</color> Light Green (<color=#FFC029>G</color>)", Format = "{0:F1}", DefaultValue = 1f, Min = 0f, Max = 1f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float green = 1f;
        [Slider("<color=#FFC029>Headlamp</color> Light Blue (<color=#FFC029>B</color>)", Format = "{0:F1}", DefaultValue = 1f, Min = 0f, Max = 1f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float blue = 1f;

        [Button(" ")]
        public void DividerA()
        {
            return;
        }

        [Slider("<color=#FFC029>Headlamp</color> Light Range", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 5f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float range = 1f;
        [Slider("<color=#FFC029>Headlamp</color> Light Intensity", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 5f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float intensity = 1f;
        [Slider("<color=#FFC029>Headlamp</color> Light Conesize", Format = "{0:F1}x", DefaultValue = 1f, Min = 0f, Max = 5f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float conesize = 1f;

        [Button(" ")]
        public void DividerB()
        {
            return;
        }

        [Toggle("<color=#FFC029>Headlamp</color> Rainbow <b>Override</b>"), OnChange(nameof(Refresh))]
        public bool rainbow = false;
        [Slider("<color=#FFC029>Headlamp</color> Rainbow Opacity", Format = "{0:F2}", DefaultValue = 1f, Min = 0.1f, Max = 1f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float rainbowOpacity = 1f;
        [Slider("<color=#FFC029>Headlamp</color> Rainbow Saturation", Format = "{0:F2}", DefaultValue = 1f, Min = 0.1f, Max = 1f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float rainbowSaturation = 1f;
        [Slider("<color=#FFC029>Headlamp</color> Loop Duration", Format = "{0:F2}", DefaultValue = 1f, Min = 0.1f, Max = 5f, Step = 0.1f), OnChange(nameof(Refresh))]
        public float rainbowDuration = 1f;

        [Button(" ")]
        public void DividerC()
        {
            return;
        }

        [Keybind("Toggle light key")]
        public KeyCode toggle = KeyCode.F;

        public void Refresh(SliderChangedEventArgs e)
        {
            foreach(HeadlampChipMono Mono in HeadlampChipMono.Headlamps) Mono.Refresh();
        }
    }
}