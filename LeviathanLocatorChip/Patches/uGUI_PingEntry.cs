using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace Ramune.LeviathanLocatorChip.Patches
{
    [HarmonyPatch(typeof(uGUI_PingEntry))]
    public static class PingEntryPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(uGUI_PingEntry), nameof(uGUI_PingEntry.Initialize))]
        public static bool Initialize(string id, bool visible, PingType type, string name, int colorIndex, uGUI_PingEntry __instance)
        {
            __instance.gameObject.SetActive(true);
            __instance.id = id;
            __instance.visibility.isOn = visible;
            __instance.visibilityIcon.sprite = (visible ? __instance.spriteVisible : __instance.spriteHidden);
            __instance.SetIcon(type);
            __instance.UpdateLabel(type, name);
            Color[] colorOptions = LeviathanLocatorChip.colors;
            int i = 0;
            int num = Mathf.Min(__instance.colorSelectors.Length, colorOptions.Length);
            while (i < num)
            {
                Toggle toggle = __instance.colorSelectors[i];
                Graphic targetGraphic = toggle.targetGraphic;
                if (targetGraphic != null)
                {
                    targetGraphic.color = colorOptions[i];
                }
                toggle.isOn = i == colorIndex;
                i++;
            }
            Color color = colorOptions[colorIndex];
            __instance.icon.SetForegroundColors(color, color, color);
            __instance.colorSelectionIndicator.position = __instance.colorSelectors[colorIndex].targetGraphic.rectTransform.position;
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(uGUI_PingEntry), nameof(uGUI_PingEntry.SetColor))]
        public static void SetColor(int index, uGUI_PingEntry __instance)
        {
            if(index < 0 || index > __instance.colorSelectors.Length) return;
            Color color = LeviathanLocatorChip.colors[index];
            __instance.icon.SetForegroundColors(color, color, color);
            __instance.colorSelectionIndicator.position = __instance.colorSelectors[index].targetGraphic.rectTransform.position;
            PingManager.SetColor(__instance.id, index);
        }
    }
}