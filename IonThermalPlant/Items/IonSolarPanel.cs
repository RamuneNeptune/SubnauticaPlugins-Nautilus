
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using RamuneLib;
using UnityEngine;
using Nautilus.Assets.Gadgets;

namespace Ramune.IonThermalPlant.Items
{
    public static class IonSolarPanel
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("IonSolarPanel", "Ion solar panel", "Procudes power from the sun", SpriteManager.Get(TechType.SolarPanel), 1, 1);

            var clone = new CloneTemplate(Info, TechType.SolarPanel)
            {
                ModifyPrefab = go =>
                {
                    var renderers = go.GetComponentsInChildren<MeshRenderer>(true);
                    foreach(var r in renderers)
                    {
                        r.material.mainTexture = Utilities.GetTexture("IonSolarPanel");
                        r.material.SetTexture("_SpecTex", Utilities.GetTexture("IonSolarPanel"));
                        r.material.SetTexture("_Illum", Utilities.GetTexture("Illum1"));
                        r.material.SetColor("_GlowColor", Color.green);
                        r.material.SetFloat("_GlowStrength", 3f);
                        r.material.SetFloat("_GlowStrengthNight", 3f);
                        r.material.EnableKeyword("MARMO_EMISSION");
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