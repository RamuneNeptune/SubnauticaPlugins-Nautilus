


namespace Ramune.RamunesOutcrops
{
    public static class Helpers
    {
        public static LootDistributionData.BiomeData[] CreateBiomeData(Dictionary<BiomeType, (int, float)> biomeDatas)
        {
            LootDistributionData.BiomeData[] biomeDatasArray = new LootDistributionData.BiomeData[biomeDatas.Count];
            int index = 0;

            foreach (var kvp in biomeDatas)
            {
                var _biome = kvp.Key;
                var _count = kvp.Value.Item1;
                var _probability = kvp.Value.Item2;

                biomeDatasArray[index] = new LootDistributionData.BiomeData { biome = _biome, count = _count, probability = _probability };
                index++;
            }
            return biomeDatasArray;
        }


        public static TechType CreateOutcrop(string id, string name, string description, TechType outcropToClone, LootDistributionData.BiomeData[] biomeData, List<BreakableResource.RandomPrefab> breakableData)
        {
            var info = Utilities.CreatePrefabInfo(id, name, description, Utilities.GetSprite(id + "Sprite"), 1, 1);
            var prefab = new CustomPrefab(info);
            var clone = new CloneTemplate(prefab.Info, outcropToClone)
            {
                ModifyPrefab = go =>
                {
                    var renderer = go.GetComponentInChildren<MeshRenderer>(true);
                    foreach(var m in renderer.materials)
                    {
                        m.mainTexture = Utilities.GetTexture(id + "Texture");
                        m.SetTexture("_SpecTex", Utilities.GetTexture(id + "Texture"));
                    }

                    var breakable = go.GetComponent<BreakableResource>();
                    breakable.defaultPrefabReference = breakableData.Last().prefabReference;
                    breakable.defaultPrefabTechType = breakableData.Last().prefabTechType;
                    breakable.breakText = $"Break {name.ToLower()}";
                    breakable.prefabList = breakableData;
                }
            };
            prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);
            prefab.SetGameObject(clone);
            prefab.SetSpawns(biomeData);
            prefab.Register();
            return prefab.Info.TechType;
        }


        public static void CreateResource(string id, string name, string description, TechType resourceToClone, Color materialColor, LootDistributionData.BiomeData[] biomeData)
        {
            var info = Utilities.CreatePrefabInfo(id, name, description, Utilities.GetSprite(id + "Sprite"), 1, 1);
            var prefab = new CustomPrefab(info);
            var clone = new CloneTemplate(prefab.Info, resourceToClone)
            {
                ModifyPrefab = go =>
                {
                    var renderers = go.GetComponentsInChildren<MeshRenderer>(true);
                    
                    foreach(var r in renderers)
                    {
                        foreach(var m in r.materials)
                        {
                            m.mainTexture = Utilities.GetTexture(id + "Texture");
                            m.SetTexture("_SpecTex", Utilities.GetTexture(id + "Texture"));
                            m.SetTexture("_Illum", Utilities.GetTexture(id + "Texture"));
                            m.SetColor("_Illum", materialColor);
                            m.color = materialColor;
                        }
                    }
                }
            };
            prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);
            prefab.SetGameObject(clone);
            prefab.SetSpawns(biomeData);
            prefab.Register();
        }
    }
}