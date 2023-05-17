
using HarmonyLib;

namespace Ramune.EnableAchievements.Patches
{
    [HarmonyPatch(typeof(GameAchievements), nameof(GameAchievements.Unlock))]
    public class GameAchievementsPatch
    {
        public static bool Prefix(GameAchievements.Id id, bool __runOriginal)
        {
            EnableAchievements.logger.LogInfo($"Unlocking '{id}'..");
            PlatformUtils.main.GetServices().UnlockAchievement(id);
            return false;
        }
    }
}