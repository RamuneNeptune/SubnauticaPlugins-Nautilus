
using HarmonyLib;
using RamuneLib;

namespace Ramune.HeadlampChip
{
    [HarmonyPatch(typeof(Player), nameof(Player.Awake))]
    public static class PlayerAwakePatch
    {
        public static void Postfix(Player __instance)
        {
            __instance.gameObject.EnsureComponent<HeadlampChipMono>();
            Utilities.Log(Colors.Amber, "HeadlampChipMono ensured on player!");
            HeadlampChip.logger.LogFatal("HeadlampChipMono ensured on player!");
        }
    }
}