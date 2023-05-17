
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using Nautilus.Crafting;
using RamuneLib;
using Nautilus.Assets.Gadgets;
using UnityEngine;
using Nautilus.Handlers;
using static CraftData;

namespace Ramune.DecoFabricator
{
    public static class Fabricator
    {
        public static void Patch()
        {
            var prefab = new CustomPrefab("DecoFabricator", "Decorations fabricator", "Used to fabricate posters, toys, caps, and more", Utilities.GetSprite(TechType.Fabricator));
            var craftTree = prefab.CreateFabricator(out CraftTree.Type craftTreeType);
            var texture = Utilities.GetTexture("  ");

            var model = new FabricatorTemplate(prefab.Info, craftTreeType)
            {
                FabricatorModel = FabricatorTemplate.Model.Fabricator,
                ModifyPrefab = obj => obj.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = texture
            };

            prefab.SetGameObject(model);
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 2), new Ingredient(TechType.Quartz, 2), new Ingredient(TechType.JeweledDiskPiece, 1)));
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            prefab.Register();

            CraftTreeHandler.AddTabNode(craftTreeType, "Posters", "Posters", SpriteManager.Get(TechType.PosterKitty));
            CraftTreeHandler.AddTabNode(craftTreeType, "Science", "Science", SpriteManager.Get(TechType.LabEquipment3));
            CraftTreeHandler.AddTabNode(craftTreeType, "Misc", "Misc", SpriteManager.Get(TechType.ArcadeGorgetoy));

            TechType[] PosterTech = { TechType.PosterKitty, TechType.Poster, TechType.PosterExoSuit1, TechType.PosterExoSuit2, TechType.PosterAurora };
            TechType[] ScienceTech = { TechType.LabEquipment3, TechType.LabEquipment2, TechType.LabEquipment1, TechType.LabContainer, TechType.LabContainer2, TechType.LabContainer3 };
            TechType[] MiscTech = { TechType.ArcadeGorgetoy, TechType.ToyCar, TechType.StarshipSouvenir, TechType.Cap2, TechType.Cap1 };

            var recipe = Utilities.CreateRecipe(1, new Ingredient(TechType.Titanium, 1));

            foreach(TechType techType in PosterTech)
            {
                CraftDataHandler.SetRecipeData(techType, recipe);
                KnownTechHandler.UnlockOnStart(techType);
                CraftTreeHandler.AddCraftingNode(craftTreeType, techType, "Posters");
            }
            foreach(TechType techType in ScienceTech)
            {
                CraftDataHandler.SetRecipeData(techType, recipe);
                KnownTechHandler.UnlockOnStart(techType);
                CraftTreeHandler.AddCraftingNode(craftTreeType, techType, "Science");
            }
            foreach(TechType techType in MiscTech)
            {
                CraftDataHandler.SetRecipeData(techType, recipe);
                KnownTechHandler.UnlockOnStart(techType);
                CraftTreeHandler.AddCraftingNode(craftTreeType, techType, "Misc");
            }
        }
    }
}