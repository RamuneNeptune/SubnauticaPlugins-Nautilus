using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using RamuneLib;
using UnityEngine;

namespace Ramune.IonThermalPlant.Items
{
    public static class IonThermalPlant
    {
        public static Texture2D IonThermalPlantTexture = Utilities.GetTexture("IonThermalPlant_");
        public static Texture2D IonThermalPlantScreenTexture = Utilities.GetTexture("IonThermalPlantScreen");

        public static PrefabInfo Info;
        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("IonThermalPlant", "Ion thermal plant", "Converts heat to energy at high efficiency.", Utilities.GetSprite("IonThermalPlantSprite"), 1, 1);
            var prefab = new CustomPrefab(Info);
            var clone = new CloneTemplate(Info, TechType.ThermalPlant)
            {
                ModifyPrefab = go =>
                {
                    var modelRoot = go.FindChild("model").FindChild("root");
                    var headRenderer = modelRoot.FindChild("head").FindChild("Thermal_reactor_head").GetComponent<MeshRenderer>();
                    var bodyRenderer = modelRoot.FindChild("Thermal_reactor_body").GetComponent<MeshRenderer>();

                    headRenderer.material.mainTexture = IonThermalPlantTexture;
                    headRenderer.material.SetTexture("_SpecTex", IonThermalPlantTexture);
                    headRenderer.material.SetColor("_GlowColor", Color.green);
                    headRenderer.material.SetFloat("_GlowStrength", 4f);

                    bodyRenderer.material.mainTexture = IonThermalPlantTexture;
                    bodyRenderer.material.SetTexture("_SpecTex", IonThermalPlantTexture);
                    bodyRenderer.material.SetColor("_GlowColor", Color.green);
                    bodyRenderer.material.SetFloat("_GlowStrength", 4f);

                    bodyRenderer.materials[1].mainTexture = IonThermalPlantScreenTexture;
                    bodyRenderer.materials[1].SetTexture("_Illum", IonThermalPlantScreenTexture);

                    go.GetComponent<PowerSource>().maxPower = 500;
                }
            };

            prefab.SetGameObject(clone);
            prefab.SetPdaGroupCategory(TechGroup.ExteriorModules, TechCategory.ExteriorModule).SetBuildable(true);

            prefab.SetUnlock(TechType.PrecursorIonBattery, 0);

            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new CraftData.Ingredient(TechType.PlasteelIngot, 1),
                new CraftData.Ingredient(TechType.Magnetite, 2),
                new CraftData.Ingredient(TechType.Aerogel, 2),
                new CraftData.Ingredient(TechType.PrecursorIonCrystal, 1)));
            prefab.Register();
        }
    }
}