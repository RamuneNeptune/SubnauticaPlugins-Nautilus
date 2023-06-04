
using System.Collections.Generic;
using HarmonyLib;
using RamuneLib;
using UnityEngine;
using static VFXParticlesPool;


namespace Ramune.LeviathanLocatorChip.Patches
{
    [HarmonyPatch(typeof(Creature), nameof(Creature.Start))]
    public static class CreaturePatch
    {
        public static List<PingInstance> pings = new List<PingInstance>();

        public static void Postfix(Creature __instance)
        {
            switch(__instance.name)
            {
                case "ReaperLeviathan(Clone)":
                    AddPing(__instance, "Reaper Leviathan", LeviathanLocatorChip.Reaper, 5);
                    break;
                case "GhostLeviathan(Clone)":
                    AddPing(__instance, "Ghost Leviathan", LeviathanLocatorChip.Ghost, 6);
                    break;
                case "GhostLeviathanJuvenile(Clone)":
                    AddPing(__instance, "Juvenile Ghost Leviathan", LeviathanLocatorChip.GhostJuvenile, 6);
                    break;
                case "SeaDragon(Clone)":
                    AddPing(__instance, "Sea Dragon", LeviathanLocatorChip.Dragon, 8);
                    break;
                case "SeaTreader(Clone)":
                    AddPing(__instance, "Sea Treader", LeviathanLocatorChip.Treader, 7);
                    break;
                default: 
                    break;
            }
        }

        public static void AddPing(Creature go, string name, PingType pingType, int colorId)
        {
            var ping = go.gameObject.EnsureComponent<PingInstance>();
            ping.displayPingInManager = false;
            ping.origin = go.transform;
            ping.SetVisible(false);
            ping.SetColor(colorId);
            ping.SetType(pingType);
            ping.SetLabel(name);
            pings.Add(ping);
        }
    }
}