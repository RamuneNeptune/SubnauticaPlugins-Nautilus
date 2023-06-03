using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using RamuneLib;
using UnityEngine;

namespace Ramune.IonThermalPlant.Items
{
    public static class IonThermalPlant
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("IonThermalPlant", "Ion thermal plant", "Procudes power from heat", Utilities.GetSprite("IonThermalPlantSprite"), 1, 1);

            var clone = new CloneTemplate(Info, TechType.ThermalPlant)
            {
                ModifyPrefab = go =>
                {
                    var renderers = go.GetComponentsInChildren<MeshRenderer>(true);
                    foreach (var r in renderers)
                    {
                        r.material.mainTexture = Utilities.GetTexture("IonThermalPlant");
                        r.material.SetTexture("_SpecTex", Utilities.GetTexture("IonThermalPlant"));
                        r.material.SetColor("_GlowColor", Color.green);
                        r.material.SetFloat("_GlowStrength", 4f);
                    }
                }
            };
            var prefab = new CustomPrefab(Info);

            prefab.SetGameObject(clone);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new CraftData.Ingredient(TechType.Quartz, 1),
                new CraftData.Ingredient(TechType.Silver, 1)));

            prefab.SetPdaGroupCategory(TechGroup.ExteriorModules, TechCategory.ExteriorModule).SetBuildable(true);
            prefab.Register();
        }
    }
}