
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using UnityEngine;
using RamuneLib;
using Nautilus.Assets.Gadgets;
using Nautilus.Handlers;

namespace Ramune.IonSolarPanel.Items
{
    public static class IonSolarPanelKit
    {
        public static PrefabInfo Info;

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("IonSolarPanelKit", "Ion solar panel kit", "A kit used to construct an ion solar panel.", Utilities.GetSprite("IonSolarPanelSprite"), 1, 1);
            CraftDataHandler.SetBackgroundType(Info.TechType, CraftData.BackgroundType.PlantAir);

            var clone = new CloneTemplate(Info, TechType.FiberMesh) { };
            var prefab = new CustomPrefab(Info);

            prefab.SetGameObject(clone);
            prefab.SetRecipe(Utilities.CreateRecipe(1,
                new CraftData.Ingredient(TechType.Quartz, 2),
                new CraftData.Ingredient(TechType.Titanium, 2),
                new CraftData.Ingredient(TechType.Silver, 1),
                new CraftData.Ingredient(IonCrystalShard.Info.TechType, 1)))
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Resources", "AdvancedMaterials");
            prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.Misc);
            prefab.Register();
        }
    }
}