
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using Nautilus.Crafting;
using static CraftData;
using RamuneLib.Utilities;
using Nautilus.Assets.Gadgets;

namespace Ramune.MoreDecoys.Items
{
    public static class GasDecoy
    {
        public static PrefabInfo info;
        public static void Patch()
        {
            RecipeData recipe = Recipe.Create(3,
                new Ingredient(TechType.Titanium, 4),
                new Ingredient(TechType.GasPod, 2),
                new Ingredient(TechType.Quartz, 2),
                new Ingredient(TechType.WiringKit, 1));

            PrefabInfo CyclopsGasDecoyInfo = PrefabInfo
                .WithTechType("CyclopsDecoyGas", "Creature gas decoy", "A decoy that deploys a gas field after a few seconds that slowly applies damage to attracted fauna.")
                .WithIcon(Sprite.Get("DecoyGas"))
                .WithSizeInInventory(new Vector2int(1, 2));
            info = CyclopsGasDecoyInfo;

            CustomPrefab CyclopsGasDecoy = new CustomPrefab(CyclopsGasDecoyInfo);
            PrefabTemplate clone = new CloneTemplate(CyclopsGasDecoyInfo, TechType.CyclopsDecoy) { };

            CyclopsGasDecoy.SetGameObject(clone);
            CyclopsGasDecoy.SetEquipment(EquipmentType.DecoySlot);
            CyclopsGasDecoy.SetRecipe(recipe)
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Resources", "AdvancedMaterials"); 

            CyclopsGasDecoy.Register();
        }
    }
}