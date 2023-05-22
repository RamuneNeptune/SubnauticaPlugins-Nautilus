
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using static CraftData;
using static RamuneLib.Utilities;

namespace Ramune.HeadlampChip
{
    public static class HeadlampChipItem
    {
        public static PrefabInfo info;
        public static void Patch()
        {
            RecipeData recipe = CreateRecipe(1,
                new Ingredient(TechType.Battery, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Lithium, 2),
                new Ingredient(TechType.AdvancedWiringKit, 1));

            PrefabInfo HeadlampChipInfo = PrefabInfo
                .WithTechType("HeadlampChip", "Headlamp Chip", "RGB capable headlamp chip implanted into the brain.")
                .WithIcon(GetSprite("HeadlampChip"))
                .WithSizeInInventory(new Vector2int(1, 1));
            info = HeadlampChipInfo;

            CustomPrefab HeadlampChip = new CustomPrefab(HeadlampChipInfo);
            PrefabTemplate clone = new CloneTemplate(HeadlampChipInfo, TechType.MapRoomHUDChip)
            {
                //ModifyPrefab = prefab => prefab.EnsureComponent<TakeDamageOnCollide>()
            };

            HeadlampChip.SetGameObject(clone);
            HeadlampChip.AddGadget(new ScanningGadget(HeadlampChip, TechType.Compass).WithPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment));
            HeadlampChip.SetEquipment(EquipmentType.Chip); 
            HeadlampChip.SetRecipe(recipe)
                .WithFabricatorType(CraftTree.Type.Fabricator) 
                .WithStepsToFabricatorTab("Personal", "Equipment");

            HeadlampChip.Register();
        }
    }
}