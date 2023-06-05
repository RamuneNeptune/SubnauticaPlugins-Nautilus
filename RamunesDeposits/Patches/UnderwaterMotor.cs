


namespace Ramune.RamunesOutcrops.Patches
{
    [HarmonyPatch(typeof(UnderwaterMotor), nameof(UnderwaterMotor.AlterMaxSpeed))]
    public static class UnderwaterMotorPatch
    {
        public static void Postfix(UnderwaterMotor __instance, ref float __result)
        {
            if(Inventory.Get().equipment.GetCount(RadiantFins.Info.TechType) > 0) __result += 5f * __instance.currentPlayerSpeedMultipler;
        }
    }
}