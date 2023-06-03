using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Ramune.LeviathanLocatorChip.Patches
{
    [HarmonyPatch(typeof(PingInstance), nameof(PingInstance.SetColor))]
    public static class PingInstancePatch
    {
        public static bool Prefix(PingInstance __instance, bool __runOriginal, int index)
        {
            if(index > LeviathanLocatorChip.colors.Length) index = 0;
            __instance.colorIndex = index;
            if(__instance.initialized) PingManager.NotifyColor(__instance);
            return false;
        }
    }
}