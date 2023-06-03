
using HarmonyLib;
using RamuneLib;
using UnityEngine;
using static VFXParticlesPool;


namespace Ramune.LeviathanLocatorChip.Patches
{
    [HarmonyPatch(typeof(Creature), nameof(Creature.Start))]
    public static class CreaturePatch
    {
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
            ping.origin = go.transform;
            ping.SetLabel(name);
            ping.SetType(pingType);
            ping.SetColor(colorId);
            ping.SetVisible(true);
            ping.displayPingInManager = false;
        }
    }
}

/*
var renderers = __instance.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);
foreach (var r in renderers)
{
    if (r.name != "Reaper_Leviathan_geo") break;
    r.material.SetTexture("_Illum", Utilities.GetTexture("Illum"));
    r.material.SetColor("_GlowColor", Color.red);
    r.material.SetFloat("_GlowStrength", 0.3f);
}
*/