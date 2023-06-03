
using System;
using System.Collections.Generic;
using System.Linq;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Extensions;
using Nautilus.Handlers;
using RamuneLib;
using UnityEngine;
using UnityEngine.Yoga;
using static LootDistributionData;

namespace Ramune.RamunesOutcrops
{
    public static class Helpers
    {
        public static BiomeData[] CreateBiomeData(Dictionary<BiomeType, (int, float)> biomeDatas)
        {
            BiomeData[] biomeDatasArray = new BiomeData[biomeDatas.Count];
            int index = 0;

            foreach (var kvp in biomeDatas)
            {
                var _biome = kvp.Key;
                var _count = kvp.Value.Item1;
                var _probability = kvp.Value.Item2;

                biomeDatasArray[index] = new BiomeData { biome = _biome, count = _count, probability = _probability };
                index++;

                RamunesOutcrops.logger.LogDebug($"{_biome}, {_count}, {_probability * 100}%");
            }
            return biomeDatasArray;
        }


        public static TechType CreateOutcrop(string id, string name, string description, TechType outcropToClone, BiomeData[] biomeData, List<BreakableResource.RandomPrefab> itemData)
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
                    breakable.defaultPrefabReference = itemData.Last().prefabReference;
                    breakable.defaultPrefabTechType = itemData.Last().prefabTechType;
                    breakable.breakText = $"Break {name}";
                    breakable.prefabList = itemData;
                }
            };
            prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.BasicMaterials);
            prefab.SetGameObject(clone);
            prefab.SetSpawns(biomeData);
            prefab.Register();

            RamunesOutcrops.logger.LogDebug($"'{id}' is registered");
            return prefab.Info.TechType;
        }


        public static void CreateResource(string id, string name, string description, TechType resourceToClone, Color materialColor, BiomeData[] biomeData)
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
            RamunesOutcrops.logger.LogDebug($"'{id}' is finished & registered");
        }
    }
}