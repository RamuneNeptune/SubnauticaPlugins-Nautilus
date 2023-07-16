

namespace Ramune.FuckingHighFOV.Patches
{
    [HarmonyPatch(typeof(Player), nameof(Player.Start))]
    public static class PlayerPatch
    {
        public static void Postfix(Player __instance)
        {
            __instance.gameObject.EnsureComponent<FOVifier>();
        }
    }

    public class FOVifier : MonoBehaviour
    { 
        public void Start()
        {
            MiscSettings.fieldOfView = 115;
        }

        public void FixedUpdate()
        {
            if(Player.main.pda.isInUse) return;
            Player.main.camRoot.SetFov(115);
        }
    }
}