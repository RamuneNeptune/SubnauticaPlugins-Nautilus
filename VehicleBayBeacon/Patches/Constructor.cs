
using BepInEx.Logging;
using UnityEngine;
using HarmonyLib;
using RamuneLib;
using BepInEx;

namespace Ramune.VehicleBayBeacon
{
    [HarmonyPatch(typeof(Constructor), nameof(Constructor.Deploy))]
    public static class ConstructorPatch
    {
        public static PingInstance ping;

        public static void Postfix(Constructor __instance, bool value)
        {
            ping = __instance.gameObject.EnsureComponent<PingInstance>();
            ping.SetLabel("Mobile Vehicle Bay");
            ping.pingType = PingType.Signal;
            ping.origin = __instance.gameObject.transform;

            if(value) ping.visible = true;
            else ping.OnDisable();
        }
    }
}