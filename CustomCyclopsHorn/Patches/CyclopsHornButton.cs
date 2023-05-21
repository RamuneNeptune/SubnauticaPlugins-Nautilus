
using HarmonyLib;
using Nautilus.Utility;

namespace Ramune.CustomCyclopsHorn.Patches
{
    [HarmonyPatch(typeof(CyclopsHornButton), nameof(CyclopsHornButton.OnPress))]
    public static class CyclopsHornPatch
    {
        public static FMOD_CustomEmitter emitter;

        public static bool Prefix(CyclopsHornButton __instance, bool __runOriginal)
        {
            if(Player.main.currentSub != __instance.subRoot) return false;
            if(emitter == null) emitter = __instance.hornSFX;
            if(CustomCyclopsHorn.sounds[CyclopsHornSoundHandler.currentIndex_] == "Default horn sound") emitter.SetAsset(AudioUtils.GetFmodAsset("event:/sub/cyclops/horn"));
            else
            {
                emitter.GetEventInstance().setVolume(CustomCyclopsHorn.config.volume);
                emitter.GetEventInstance().setPitch(CustomCyclopsHorn.config.pitch);

                emitter.SetAsset(AudioUtils.GetFmodAsset(CustomCyclopsHorn.sounds[CyclopsHornSoundHandler.currentIndex_]));

                emitter.GetEventInstance().setVolume(CustomCyclopsHorn.config.volume);
                ErrorMessage.AddError($"Set volume to {CustomCyclopsHorn.config.volume}");
                emitter.GetEventInstance().setPitch(CustomCyclopsHorn.config.pitch);
                ErrorMessage.AddError($"Set pitch to {CustomCyclopsHorn.config.pitch}");
            }
            emitter.Play();
            emitter.GetEventInstance().setVolume(CustomCyclopsHorn.config.volume);
            emitter.GetEventInstance().setPitch(CustomCyclopsHorn.config.pitch);
            return false;
        }
    }
}