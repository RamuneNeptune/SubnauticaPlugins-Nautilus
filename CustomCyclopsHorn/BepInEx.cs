
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using Nautilus.Handlers;
using Nautilus.Utility;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using RamuneLib;
using FMOD;
using Nautilus.Json;
using Nautilus.Options.Attributes;
using UnityEngine;
using UnityEngineInternal.Input;
using System.Diagnostics;
using Nautilus.Options;

namespace Ramune.CustomCyclopsHorn
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    [BepInProcess("Subnautica.exe")]
    public class CustomCyclopsHorn : BaseUnityPlugin
    {
        internal static Options config { get; } = OptionsPanelHandler.RegisterModOptions<Options>();

        private const string myGUID = "com.ramune.CustomCyclopsHorn";
        private const string pluginName = "Custom Cyclops Horn";
        private const string versionString = "1.0.0";
        private static readonly Harmony harmony = new Harmony(myGUID);
        public static ManualLogSource logger;

        public static List<string> sounds = new List<string>() { "Default horn sound" };
        public static string MP3Folder = IOUtilities.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Sounds");
        public static int selectedSound;

        public void Awake()
        {
            harmony.PatchAll();
            Main.FindPiracy();
            SetupSounds();
            Logger.LogInfo(pluginName + " " + versionString + " " + "has been loaded! (yay)");
            logger = Logger;
        }

        public void SetupSounds()
        {
            var files = Directory.GetFiles(IOUtilities.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Sounds"));
            foreach (var file in files)
            {
                var filename = Path.GetFileName(file);
                if(sounds.Contains(filename)) break;
                if(!file.EndsWith(".mp3"))
                {
                    logger.LogError($"Incorrect file in 'Sounds' folder at: {file}");
                    break;
                }
                Sound sound = AudioUtils.CreateSound(file, MODE._3D);
                CustomSoundHandler.RegisterCustomSound(filename, sound, AudioUtils.BusPaths.PlayerSFXs);
                sounds.Add(filename);
            }
        }
    }
    
    [Menu("Custom Cyclops Horn")]
    public class Options : ConfigFile
    {
        
        [Button("Open sounds (mp3) folder")]
        public void Open(ButtonClickedEventArgs _)
        {
            Process.Start(CustomCyclopsHorn.MP3Folder);
        }
        
        [Keybind("Next sound key")]
        public KeyCode nextSound = KeyCode.Period;

        [Slider("Custom horn pitch", Format = "{0:0}%", DefaultValue = 100f, Min = 1f, Max = 400f, Step = 1f, Tooltip = "E.g. setting this to '200%', would be very high pitch")]
        public float pitch = 100f;

        [Slider("Custom horn volume", Format = "{0:0}%", DefaultValue = 100f, Min = 1f, Max = 250f, Step = 1f, Tooltip = "E.g. setting this to '200%', would be double volume")]
        public float volume = 100f;
    }
}