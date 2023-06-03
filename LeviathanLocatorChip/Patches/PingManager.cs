using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Ramune.LeviathanLocatorChip.Patches
{
    [HarmonyPatch(typeof(PingManager), nameof(PingManager.NotifyColor))]
    public static class PingManagerPatch
    {
        public static bool Prefix(PingInstance instance, bool __runOriginal)
        {
            if(instance == null) return false;
            if(PingManager.onColor != null)
            {
                int num = instance.colorIndex;
                if(num < 0 || num > LeviathanLocatorChip.colors.Length) num = 0;
                Color color = LeviathanLocatorChip.colors[num];
                PingManager.onColor(instance.Id, color);
            }
            return false;
        }
    }
}