using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HarmonyLib;
using Mono.Cecil.Cil;
using UnityEngine;
using static VFXParticlesPool;

namespace Ramune.AchievementUnlocker.Patches
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel), nameof(uGUI_OptionsPanel.AddTabs))]
    public class uGUI_OptionsPanelPatch
    {
        public static string UnlockerTabName = "Unlocker";
        public static int UnlockerTab;

        public static void Postfix(uGUI_OptionsPanel __instance)
        {
            UnlockerTab = __instance.AddTab(UnlockerTabName);
            __instance.AddHeading(UnlockerTab, "\n<color=#f1c232>IMPORTANT - Read Me Please</color>\nClick the button below to open the Wiki page for Subnautica Achievements, it will useful to know what achievements your unlocking"); Divider(__instance);
            __instance.AddButton(UnlockerTab, "Open Achievement Wiki (in browser)\n", () =>
            {
                Process.Start("https://subnautica.fandom.com/wiki/Achievements#Subnautica");
                AchievementUnlocker.logger.LogInfo("Opened Achievement Wiki page in browser");
            }); Divider(__instance);

            __instance.AddHeading(UnlockerTab, "<color=#f1c232>Achievements</color>\nClick any button below to unlock the corresponding achievement\n"); Divider(__instance);


            __instance.AddButton(UnlockerTab, "<color=#ffb717><b>Unlock All</b></color>", () =>
            {
                foreach (GameAchievements.Id achievement in Enum.GetValues(typeof(GameAchievements.Id))) PlatformUtils.main.GetServices().UnlockAchievement(achievement);
                ErrorMessage.AddError("<color=#ffb717>Unlocked:</color> All achievements\nThis may take some time, be patient");
            });

            __instance.AddButton(UnlockerTab, "<color=#ffb717><b>Reset All</b></color>", () =>
            {
                PlatformUtils.main.GetServices().ResetAchievements();
                ErrorMessage.AddError("<color=#ffb717>Reset:</color> All achievements");
            });

            foreach (GameAchievements.Id achievement in Enum.GetValues(typeof(GameAchievements.Id))) if (achievement != GameAchievements.Id.None) Button(achievement, __instance);

            Divider(__instance);
        }

        public static void Divider(uGUI_OptionsPanel instance) => instance.AddHeading(UnlockerTab, " ");
        public static void Button(GameAchievements.Id id, uGUI_OptionsPanel instance)
        {
            var stringID = Regex.Replace(id.ToString(), "(\\B[A-Z])", " $1");
            instance.AddButton(UnlockerTab, stringID, () =>
            {
                PlatformUtils.main.GetServices().UnlockAchievement(id);
                ErrorMessage.AddError($"<color=#ffa618>Unlocked:</color> {stringID}");
            });
        }
    }
}