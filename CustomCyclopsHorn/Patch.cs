
using HarmonyLib;
using Nautilus.Utility;
using UnityEngine;

namespace Ramune.CustomCyclopsHorn
{
    [HarmonyPatch(typeof(Player), nameof(Player.Awake))]
    public static class PlayerPatch
    {
        public static void Postfix(Player __instance)
        {
            __instance.gameObject.EnsureComponent<CyclopsHornSoundHandler>();
        }
    }

    [HarmonyPatch(typeof(CyclopsHornButton), nameof(CyclopsHornButton.OnPress))]
    public static class CyclopsHornPatch
    {
        public static FMOD_CustomEmitter emitter;

        public static bool Prefix(CyclopsHornButton __instance, bool __runOriginal)
        {
            if (Player.main.currentSub != __instance.subRoot) return false;
            if (emitter == null) emitter = __instance.hornSFX;
            if (CustomCyclopsHorn.sounds[CyclopsHornSoundHandler.currentIndex_] == "Default horn sound") emitter.SetAsset(AudioUtils.GetFmodAsset("event:/sub/cyclops/horn"));
            else
            {
                emitter.SetAsset(AudioUtils.GetFmodAsset(CustomCyclopsHorn.sounds[CyclopsHornSoundHandler.currentIndex_]));
                emitter.GetEventInstance().setVolume(CustomCyclopsHorn.config.volume);
                emitter.GetEventInstance().setPitch(CustomCyclopsHorn.config.pitch);
            }
            emitter.Play();
            return false;
        }
    }
}