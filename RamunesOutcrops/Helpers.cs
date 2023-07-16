
using CuddleLibs.Utility;


namespace Ramune.RamunesOutcrops
{
    public static class Helpers
    {
        public static TechType CreateOutcrop(string id, string name, string description, TechType toClone, LootDistributionData.BiomeData[] biomeData, params(TechType, TechType, float) []dropData)
        {
            var info = Utilities.CreatePrefabInfo(id, name, description, Utilities.GetSprite($"{id}Sprite"), 1, 1);
            var prefab = new CustomPrefab(info);
            var clone = new CloneTemplate(prefab.Info, toClone)
            {
                ModifyPrefab = go =>
                {
                    var renderer = go.GetComponentInChildren<MeshRenderer>(true);
                    foreach (var m in renderer.materials)
                    {
                        m.SetTexture(ShaderPropertyID._MainTex, Utilities.GetTexture($"{id}Texture"));
                        m.SetTexture(ShaderPropertyID._SpecTex, Utilities.GetTexture($"{id}Texture"));
                    }
                    var breakable = go.GetComponent<BreakableResource>();
                    breakable.breakText = $"Break {name.ToLower()}";
                    breakable.RemoveVanillaDrops();
                }
            };
            prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);
            prefab.SetGameObject(clone);
            prefab.SetSpawns(biomeData);
            prefab.Register();

            OutcropsUtils.EnsureOutcropDrop(dropData);

            return prefab.Info.TechType;
        }

        public static LootDistributionData.BiomeData[] CreateBiomeData(Dictionary<BiomeType, (int, float)> biomeDatas)
        {
            LootDistributionData.BiomeData[] biomeDatasArray = new LootDistributionData.BiomeData[biomeDatas.Count];
            int index = 0;

            foreach(var kvp in biomeDatas)
            {
                var _biome = kvp.Key;
                var _count = kvp.Value.Item1;
                var _probability = kvp.Value.Item2;

                biomeDatasArray[index] = new LootDistributionData.BiomeData { biome = _biome, count = _count, probability = _probability };
                index++;
            }
            return biomeDatasArray;
        }
    }
}