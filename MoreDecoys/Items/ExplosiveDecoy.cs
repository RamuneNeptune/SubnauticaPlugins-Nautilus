
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using Nautilus.Crafting;
using static CraftData;
using RamuneLib.Utils;
using Nautilus.Assets.Gadgets;

namespace Ramune.MoreDecoys.Items
{
    public static class ExplosiveDecoy
    {
        public static PrefabInfo info;
        public static void Patch()
        {
            RecipeData recipe = Recipe.Create(3,
                new Ingredient(TechType.Titanium, 5),
                new Ingredient(TechType.WiringKit, 1),
                new Ingredient(TechType.CrashPowder, 2),
                new Ingredient(TechType.Quartz, 1));

            PrefabInfo CyclopsExplosiveDecoyInfo = PrefabInfo
                .WithTechType("CyclopsDecoyExplosive", "Creature explosive decoy", "A decoy that deploys an explosive after a few seconds.")
                .WithIcon(Sprite.Get("DecoyExplosive"))
                .WithSizeInInventory(new Vector2int(1, 2));
            info = CyclopsExplosiveDecoyInfo;

            CustomPrefab CyclopsExplosiveDecoy = new CustomPrefab(CyclopsExplosiveDecoyInfo);
            PrefabTemplate clone = new CloneTemplate(CyclopsExplosiveDecoyInfo, TechType.CyclopsDecoy) {}; // Get a clone of CyclopsDecoy

            CyclopsExplosiveDecoy.SetGameObject(clone); // Set item prefab to the CyclopsDecoy clone
            CyclopsExplosiveDecoy.SetEquipment(EquipmentType.DecoySlot); // Set item to be used in Decoy slots
            CyclopsExplosiveDecoy.SetRecipe(recipe) // Set recipe with the one made earlier
                .WithFabricatorType(CraftTree.Type.Fabricator) // Set the type, e.g. workbench, moonpool
                .WithStepsToFabricatorTab("Resources", "AdvancedMaterials"); // Steps to crafting tab node, self-explanatory 

            CyclopsExplosiveDecoy.Register(); // The Nautilus method to patch your item
        }
    }
}