

using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using static CraftData;
using RamuneLib;
using Nautilus.Crafting;
using System.Collections.Generic;

namespace Ramune.BZEnameledGlass.Items
{
    public static class AltEnameledGlass
    {
        public static void Patch()
        {
            var prefab = new CustomPrefab("NewEnameledGlass", "Enameled Glass", "Glass, hardened using a natural substrate.", Utilities.GetSprite(TechType.EnameledGlass));
            var clone = new CloneTemplate(prefab.Info, TechType.EnameledGlass);
            prefab.SetGameObject(clone);
            prefab.SetRecipe(new RecipeData
            {
                craftAmount = 0,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.Glass, 1),
                    new Ingredient(TechType.Lead, 1),
                    new Ingredient(TechType.Diamond, 1)
                },
                LinkedItems = new List<TechType>
                {
                    TechType.EnameledGlass
                }
            })
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab("Resources", "BasicMaterials");
            prefab.Register();
        }
    }
}