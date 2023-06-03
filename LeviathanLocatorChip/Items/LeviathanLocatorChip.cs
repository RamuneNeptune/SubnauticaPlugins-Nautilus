
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using static CraftData;
using static RamuneLib.Utilities;

namespace Ramune.LeviathanLocatorChip.Items
{
    public static class LeviathanLocatorChip
    {
        public static PrefabInfo Info;
        public static void Patch()
        {
            RecipeData recipe = CreateRecipe(1,
                new Ingredient(TechType.Battery, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Lithium, 2),
                new Ingredient(TechType.AdvancedWiringKit, 1));

            PrefabInfo LeviathanLocatorChipInfo = PrefabInfo
                .WithTechType("LeviathanLocatorChip", "Leviathan Locator Chip", "Gotta catch 'em all.")
                .WithIcon(GetSprite(TechType.SeaDragon))
                .WithSizeInInventory(new Vector2int(1, 1));
            Info = LeviathanLocatorChipInfo;

            CustomPrefab LeviathanLocatorChip = new CustomPrefab(LeviathanLocatorChipInfo);
            PrefabTemplate clone = new CloneTemplate(LeviathanLocatorChipInfo, TechType.MapRoomHUDChip) { };

            LeviathanLocatorChip.SetGameObject(clone);
            LeviathanLocatorChip.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);
            LeviathanLocatorChip.SetEquipment(EquipmentType.Chip);
            LeviathanLocatorChip.SetRecipe(recipe)
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Personal", "Equipment");

            LeviathanLocatorChip.Register();
        }
    }
}