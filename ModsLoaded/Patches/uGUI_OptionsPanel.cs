using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;

namespace Ramune.ModsLoaded.Patches
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    public static class uGUI_OptionsPanelPatches
    {
        public static string[] array = Chainloader.PluginInfos.Select(kvp => kvp.Value.Metadata.Name).ToArray();
        public static string names = string.Join(Environment.NewLine, names.Select(name => "Loaded: " + name));
        public static string modsLoaded = names.Length.ToString();

        public static string tabName = "Mods Loaded" + " (" + modsLoaded + ")";
        public static bool toggle;
        public static int tab;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(uGUI_OptionsPanel), nameof(uGUI_OptionsPanel.AddTabs))]
        internal static void Postfix(uGUI_OptionsPanel __instance)
        {
            tab = __instance.AddTab(tabName);

            __instance.AddHeading(tab, "\n<color=#f1c232>Mods Loaded</color>\nHere you will get a list of mods loaded every time you launch, and also you can click the buttons below to open your Mods folder, open your logfile, and display all the mods loaded currently onto your screen");
            __instance.AddHeading(tab, " ");

            __instance.AddButton(tab, "Open mods folder", () => {
                Process.Start(Paths.PluginPath);
            });

            __instance.AddButton(tab, "Open log file", () => {
                Process.Start(Path.Combine(Paths.BepInExRootPath, "LogOutput.log"));
            });

            __instance.AddHeading(tab, " ");
            __instance.AddHeading(tab, modsLoaded + " mods have been loaded");

            foreach(var name in names)
            {
                __instance.AddHeading(tab, "<size=5%><color=#f1c232>Loaded:</color> <color=#d9dcdc>" + name + "</color></size>");
            }

            __instance.AddHeading(tab, " ");
        }
    
        [HarmonyPrefix]
        [HarmonyPatch(typeof(uGUI_OptionsPanel), nameof(uGUI_OptionsPanel.AddKeyRedemptionTab))]
        public static bool AddKeyRedemptionTab(ref bool __runOriginal)
        {
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(uGUI_OptionsPanel), nameof(uGUI_OptionsPanel.AddTroubleshootingTab))]
        public static bool AddTroubleshootingTab(ref bool __runOriginal)
        {
            return false;
        }
    }
}