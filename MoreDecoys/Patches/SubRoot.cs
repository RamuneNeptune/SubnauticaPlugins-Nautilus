
using System.Collections;
using HarmonyLib;
using UWE;

namespace Ramune.MoreDecoys.Patches
{
    [HarmonyPatch(typeof(SubRoot), nameof(SubRoot.Awake))]
    public static class SubRootPatch
    {
        public static void Postfix(SubRoot __instance)
        {
            if (!__instance.isCyclops) return;
            CoroutineHost.StartCoroutine(WaitForButton(__instance));
        }

        public static IEnumerator WaitForButton(SubRoot subRoot)
        {
            var button = subRoot.gameObject.GetComponentInChildren<CyclopsDecoyLaunchButton>();
            while (button == null)
            {
                yield return null;
                button = subRoot.gameObject.GetComponentInChildren<CyclopsDecoyLaunchButton>();
            }
            if (button != null) button.cooldown = 3.3f;
        }
    }
}