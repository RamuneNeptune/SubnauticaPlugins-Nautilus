
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using RamuneLib;
using UnityEngine;
using Nautilus.Assets.Gadgets;

namespace Ramune.IonSolarPanel.Items
{
    public static class IonSolarPanel
    {
        public static PrefabInfo Info;
        public static Texture2D Texture = Utilities.GetTexture("IonSolarPanel");
        public static Texture2D Illum = Utilities.GetTexture("Illum");
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("IonSolarPanel", "Ion solar panel", "Procudes power from the sun", Utilities.GetSprite("IonSolarPanelSprite"), 1, 1);

            var clone = new CloneTemplate(Info, TechType.SolarPanel)
            {
                ModifyPrefab = go =>
                {
                    var renderers = go.GetComponentsInChildren<MeshRenderer>(true);
                    foreach(var r in renderers)
                    {
                        r.material.mainTexture = Texture;
                        r.material.SetTexture("_SpecTex", Texture);
                        r.material.SetTexture("_Illum", Illum);
                        r.material.SetColor("_GlowColor", Color.green);
                        r.material.SetFloat("_GlowStrength", 2f);
                        r.material.SetFloat("_GlowStrengthNight", 3f);
                        r.material.EnableKeyword("MARMO_EMISSION");
                    }
                }
            };
            var prefab = new CustomPrefab(Info);

            prefab.SetGameObject(clone);
            prefab.SetRecipe(Utilities.CreateRecipe(1, new CraftData.Ingredient(IonSolarPanelKit.Info.TechType, 1)));
            prefab.SetPdaGroupCategory(TechGroup.ExteriorModules, TechCategory.ExteriorModule).SetBuildable(true);
            prefab.Register();
        }
    }
}