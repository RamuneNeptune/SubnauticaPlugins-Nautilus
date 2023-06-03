using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Ramune.LeviathanLocatorChip.Patches
{
    [HarmonyPatch(typeof(uGUI_Pings), nameof(uGUI_Pings.OnAdd))]
    public static class PingsPatch
    {
        public static bool Prefix(PingInstance instance, uGUI_Pings __instance)
        {
            uGUI_Ping uGUI_Ping = __instance.poolPings.Get();
            uGUI_Ping.Initialize();
            uGUI_Ping.SetVisible(instance.visible);
            uGUI_Ping.SetColor(LeviathanLocatorChip.colors[instance.colorIndex]);
            uGUI_Ping.SetIcon(SpriteManager.Get(SpriteManager.Group.Pings, PingManager.sCachedPingTypeStrings.Get(instance.pingType)));
            uGUI_Ping.SetLabel(instance.GetLabel());
            uGUI_Ping.SetIconAlpha(0f);
            uGUI_Ping.SetTextAlpha(0f);
            __instance.pings.Add(instance.Id, uGUI_Ping);
            return false;
        }
    }
}