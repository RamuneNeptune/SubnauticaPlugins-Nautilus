
using HarmonyLib;

namespace Ramune.DeveloperMode.Patches
{
    [HarmonyPatch(typeof(IngameMenu), nameof(IngameMenu.Open))]
    public static class IngameMenu_Patch
    {
        public static void Postfix()
        {
            IngameMenu.main.developerMode = true;
            IngameMenu.main.developerButton.gameObject.SetActive(true);
        }
    }
}