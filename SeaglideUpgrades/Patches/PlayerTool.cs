
using HarmonyLib;
using Ramune.SeaglideUpgrades.Items;
using RamuneLib;
using UnityEngine;

namespace Ramune.SeaglideUpgrades
{
    [HarmonyPatch(typeof(PlayerTool))]
    public static class PlayerToolPatches
    {
        public static PlayerController controller = Player.main.GetComponent<PlayerController>();
        public static Light[] MK1_lights;
        public static Light[] MK2_lights;
        public static Light[] MK3_lights;
        public static float MK1_speed = 41f;
        public static float MK2_speed = 47f;
        public static float MK3_speed = 53f;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerTool), nameof(PlayerTool.animToolName), MethodType.Getter)]
        public static void AnimPostfix(PlayerTool __instance, ref string __result)
        {
            if(__instance.pickupable?.GetTechType() == MK1.Info.TechType) __result = "seaglide";
            if(__instance.pickupable?.GetTechType() == MK2.Info.TechType) __result = "seaglide";
            if(__instance.pickupable?.GetTechType() == MK3.Info.TechType) __result = "seaglide";
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlayerTool), nameof(PlayerTool.OnDraw))]
        public static void DrawPrefix(PlayerTool __instance, Player p)
        {
            if(__instance.GetType().Name != "Seaglide") return;
            switch(__instance.gameObject.name)
            {
                case("SeaGlide(Clone)"): // Seaglide
                    Utilities.Log(Colors.Blue, "Setting DEFAULT");
                    controller.seaglideForwardMaxSpeed = MK1_speed;
                    controller.seaglideWaterAcceleration = MK1_speed;
                    break;
                case("SeaglideMK1(Clone)"): // MK1
                    Utilities.Log(Colors.Cyan, "Setting MK1");
                    if(MK1_lights != null) MK1_lights = __instance.gameObject.GetComponentsInChildren<Light>(true);
                    controller.seaglideForwardMaxSpeed = MK1_speed;
                    controller.seaglideWaterAcceleration = MK1_speed;
                    break;

                case("SeaglideMK2(Clone)"): // MK2
                    Utilities.Log(Colors.Lime, "Setting MK2");
                    if(MK2_lights != null) MK1_lights = __instance.gameObject.GetComponentsInChildren<Light>(true);
                    controller.seaglideForwardMaxSpeed = MK2_speed;
                    controller.seaglideWaterAcceleration = MK2_speed;
                    break;

                case("SeaglideMK3(Clone)"): // MK3
                    Utilities.Log(Colors.Red, "Setting MK3");
                    if(MK3_lights != null) MK1_lights = __instance.gameObject.GetComponentsInChildren<Light>(true);
                    controller.seaglideForwardMaxSpeed = MK3_speed;
                    controller.seaglideWaterAcceleration = MK3_speed;
                    break;
            }
        }
    }
}