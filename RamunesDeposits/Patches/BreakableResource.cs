


namespace Ramune.RamunesOutcrops.Patches
{
    [HarmonyPatch(typeof(BreakableResource))]
    public static class BreakablePatch
    {
        public static Dictionary<TechType, List<BreakableResource.RandomPrefab>> prefabs = new Dictionary<TechType, List<BreakableResource.RandomPrefab>>();

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BreakableResource), nameof(BreakableResource.SpawnResourceFromPrefab), new Type[] { typeof(AssetReferenceGameObject), typeof(Vector3), typeof(Vector3) })]
        public static void SpawnResourceFromPrefab(AssetReferenceGameObject breakPrefab, Vector3 position, Vector3 up)
        {
            ErrorMessage.AddError($"{breakPrefab}");
            logger.LogFatal(breakPrefab);
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(BreakableResource), nameof(BreakableResource.BreakIntoResources))]
        public static void BreakIntoResources(BreakableResource __instance)
        {
            foreach(var p in __instance.prefabList) Utilities.Log(Colors.Black, $"{p.prefabTechType} : {p.chance}");
        }
    }
}