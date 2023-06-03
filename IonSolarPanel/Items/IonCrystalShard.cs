
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using UnityEngine;
using RamuneLib;
using Nautilus.Assets.Gadgets;

namespace Ramune.IonSolarPanel.Items
{
    public static class IonCrystalShard
    {
        public static PrefabInfo Info;

        public static void Patch()
        {
            Info = Utilities.CreatePrefabInfo("IonCrystalShard", "Ion crystal shard", "An ion crystal piece", Utilities.GetSprite("IonCrystalShard"), 1, 1);

            var clone = new CloneTemplate(Info, TechType.PrecursorIonCrystal) { };
            var prefab = new CustomPrefab(Info);

            prefab.SetGameObject(clone);
            prefab.SetRecipe(Utilities.CreateRecipe(2, new CraftData.Ingredient(TechType.PrecursorIonCrystal, 1)))
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Resources", "AdvancedMaterials");
            prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.Misc);
            prefab.Register();
        }
    }
}