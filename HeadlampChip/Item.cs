
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using RamuneLib.Utils;
using static CraftData;

namespace Ramune.HeadlampChip
{
    public static class HeadlampChipItem
    {
        public static void Patch()
        {
            RecipeData recipe = Recipe.Create(3,
                new Ingredient(TechType.Lithium, 2),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Battery, 1),
                new Ingredient(TechType.AdvancedWiringKit, 1));

            PrefabInfo HeadlampChipInfo = PrefabInfo
                .WithTechType("HeadlampChip", "Headlamp Chip", "Headlamp chip implanted into the brain.")
                .WithIcon(Sprite.Get(TechType.Marki1))
                .WithSizeInInventory(new Vector2int(1, 2));

            CustomPrefab HeadlampChip = new CustomPrefab(HeadlampChipInfo);
            PrefabTemplate clone = new CloneTemplate(HeadlampChipInfo, TechType.MapRoomHUDChip)
            {
                ModifyPrefab = prefab => prefab.EnsureComponent<TakeDamageOnCollide>()
            };

            HeadlampChip.SetGameObject(clone);
            HeadlampChip.SetEquipment(EquipmentType.Chip); 
            HeadlampChip.SetRecipe(recipe)
                .WithFabricatorType(CraftTree.Type.Fabricator) 
                .WithStepsToFabricatorTab("Resources", "Electronics"); 
        }
    }
}