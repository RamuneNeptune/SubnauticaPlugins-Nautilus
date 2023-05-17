
using HarmonyLib;

namespace Ramune.CustomCyclopsHorn.Patches
{
    [HarmonyPatch(typeof(Player), nameof(Player.Awake))]
    public static class PlayerPatch
    {
        public static void Postfix(Player __instance)
        {
            __instance.gameObject.EnsureComponent<CyclopsHornSoundHandler>();
        }
    }
}